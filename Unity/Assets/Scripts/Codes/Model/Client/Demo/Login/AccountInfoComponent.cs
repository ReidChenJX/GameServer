namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AccountInfoComponent : Entity, IAwake, IDestroy
    {
        // Realm 登录时
        public string LoginName { get; set; }
        public long AccountId { get; set; }
        public string Token { get; set; }
        // Gate 登录时
        public string Key { get; set; }
    }
}