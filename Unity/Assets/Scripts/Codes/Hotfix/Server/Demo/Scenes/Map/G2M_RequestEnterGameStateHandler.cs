namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class G2M_RequestEnterGameStateHandler: AMActorLocationRpcHandler<Unit, G2M_RequestEnterGameState,M2G_RequestEnterGameState>
    {
        protected override async ETTask Run(Unit unit, G2M_RequestEnterGameState request, M2G_RequestEnterGameState response)
        {
            await ETTask.CompletedTask;
        }
    }
}

