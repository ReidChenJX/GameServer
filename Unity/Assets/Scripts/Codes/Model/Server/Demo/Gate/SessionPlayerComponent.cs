namespace ET.Server
{
    [ComponentOf(typeof (Session))]
    public class SessionPlayerComponent: Entity, IAwake, IDestroy
    {
        // PlayerId = AccountId
        public long PlayerId { get; set; }
        public long PlayerInstanceId { get; set; }

        public bool isLoginAgain { get; set; } = false;
    }
}