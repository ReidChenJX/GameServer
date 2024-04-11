namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class G2M_RequestExitGameHandler: AMActorLocationRpcHandler<Unit, G2M_RequestExitGame, M2G_RequestExitGame>
    {
        protected override async ETTask Run(Unit unit, G2M_RequestExitGame request, M2G_RequestExitGame response)
        {
            // 保存玩家数据至数据库，执行下线操作
            
            
            // 释放 游戏服 Unit
            await unit.RemoveLocation();
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitComponent.Remove(unit.Id);
            await ETTask.CompletedTask;
        }
    }
}


