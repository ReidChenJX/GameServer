using System;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class C2G_EnterGameHandler: AMRpcHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response)
        {
            // 锁定第一次请求
            if (session.GetComponent<SessionLockComponent>() != null)
            {
                // session 在第一次请求时挂载SessionLockComponent 组件，后续请求判断此时已挂载，则自动取消
                response.Error = ErrorCode.ERR_RequestRespeated;
                session.Disconnect().Coroutine();
                return;
            }

            Player player = session.GetComponent<SessionPlayerComponent>().GetMyPlayer();
            if (player == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                return;
            }

            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLockComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.AccountId.GetHashCode()))
            {
                if (instanceId != session.InstanceId)
                {
                    return;
                }

                if (session.GetComponent<SessionStateComponent>() != null && session.GetComponent<SessionStateComponent>().State == SessionState.Game)
                {
                    // 已经进入游戏
                    response.Error = ErrorCode.ERR_SessionStateErrror;
                    return;
                }

                if (player.PlayerState == PlayerState.Game)
                {
                    // 如果存在多个玩家相互顶号，player 需要动态调整对应的Session
                    // 出现顶号操作，Session 变化，Player 不变，但是Player 与Session 的映射发生变化
                    try
                    {
                        // 向Map 服务器请求，处理顶号/二次登录操作
                        IActorResponse reqEnter = await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestEnterGameState());

                        if (reqEnter.Error == ErrorCode.ERR_Success)
                        {
                            return;
                        }
                        
                        Log.Error("二次登录失败 " + reqEnter.Error + "|" + reqEnter.Message);
                        response.Error = ErrorCode.ERR_ReEnterGameErrorGame;
                        await DisconnectHelper.KickPlayer(player, isException: true);
                        session?.Disconnect().Coroutine();

                    }
                    catch (Exception e)
                    {
                        Log.Error("二次登录失败 " + e.ToString());
                        response.Error = ErrorCode.ERR_ReEnterGameErrorGate;
                        await DisconnectHelper.KickPlayer(player, isException: true);
                        session?.Disconnect().Coroutine();
                    }
                    
                    return;
                }

                try
                {
                    // 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
                    GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
                    gateMapComponent.Scene = await SceneFactory.CreateServerScene(gateMapComponent, player.AccountId,
                        IdGenerater.Instance.GenerateInstanceId(), gateMapComponent.DomainZone(), "GateMap", SceneType.Map);

                    Scene scene = gateMapComponent.Scene;

                    // 这里可以从DB中加载Unit
                    Unit unit = UnitFactory.Create(scene, player.AccountId, UnitType.Player);
                    unit.AddComponent<UnitGateComponent, long>(session.InstanceId);

                    long unitid = unit.Id;

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Map1");

                    response.MyId = unitid;
                    player.UnitId = unitid;

                    // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
                    // TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.InstanceId, startSceneConfig.Name).Coroutine();
                    await TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.InstanceId, startSceneConfig.Name);

                    SessionStateComponent sessionStateComponent = session.GetComponent<SessionStateComponent>();
                    if (sessionStateComponent == null)
                    {
                        sessionStateComponent = session.AddComponent<SessionStateComponent>();
                    }

                    sessionStateComponent.State = SessionState.Game;
                    player.PlayerState = PlayerState.Game;
                }
                catch (Exception e)
                {
                    Log.Error($"角色进入游戏逻辑服发生错误 账户ID: {player.AccountId}, 异常信息： {e.ToString()}");
                    response.Error = ErrorCode.ERR_OERR;
                    // 发生错误，玩家下线
                    await DisconnectHelper.KickPlayer(player, isException: true);
                    session?.Disconnect().Coroutine();
                }
            }
        }
    }
}