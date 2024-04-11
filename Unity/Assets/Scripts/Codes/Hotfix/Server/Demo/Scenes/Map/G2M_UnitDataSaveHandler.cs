namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class G2M_UnitDataSaveHandler : AMActorLocationRpcHandler<Unit, G2M_UnitDataSave, M2G_UnitDataSave>
    {
        protected override async ETTask Run(Unit unit, G2M_UnitDataSave request, M2G_UnitDataSave response)
        {
            // 保存玩家数据至数据库，执行下线操作
            await ETTask.CompletedTask;
        }
    }
}

