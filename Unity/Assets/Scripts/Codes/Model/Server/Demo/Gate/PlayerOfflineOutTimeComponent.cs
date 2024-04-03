namespace ET.Server
{
    [ComponentOf]
    public class PlayerOfflineOutTimeComponent : Entity, IAwake, IDestroy
    {
        public long Timer;
    }
}

