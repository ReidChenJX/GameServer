namespace ET.Server
{
    [ActorMessageHandler(SceneType.Gate)]
    public class L2G_DisconnectGateUnitHandler : AMActorRpcHandler<Scene, L2G_DisconnectGateUnit, G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response)
        {

            long accountId = request.AccountId;

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, accountId.GetHashCode()))
            {
                // Gate 上的玩家管理组件
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player= playerComponent.Get(accountId);

                if(player == null)
                {
                    return;
                }
                
                // 保存游戏数据
                M2G_UnitDataSave g2LUnitDataSave = (M2G_UnitDataSave)await MessageHelper.CallLocationActor(player.UnitId, new G2M_UnitDataSave());
                
                // Gate 网关断开Player
                scene.GetComponent<GateSessionKeyComponent>().Remove(accountId);
                
                // Session 断开
                Session gateSession = Root.Instance.Get(player.SessionInstanceId) as Session;
                
                if (gateSession != null && !gateSession.IsDisposed)
                {
                    if (gateSession.GetComponent<SessionPlayerComponent>() != null)
                    {
                        gateSession.GetComponent<SessionPlayerComponent>().isLoginAgain = true;
                    }
                    
                    gateSession.Send(new R2C_Disconnect() {Error = ErrorCode.ERR_ExtraAccount});
                    gateSession?.Disconnect().Coroutine();
                }

                player.SessionInstanceId = 0;

                // 玩家下线计时操作
                player.AddComponent<PlayerOfflineOutTimeComponent>();

            }
        }
    }
}