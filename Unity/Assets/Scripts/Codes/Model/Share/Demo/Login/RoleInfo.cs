namespace ET
{
    public enum RoleState
    {
        Normal,
        Freeze,
    }

    [ChildOf]
    public class RoleInfo: Entity, IAwake, IDestroy
    {
        public string RoleName { get; set; }
        public int State { get; set; }
        public long AccountId { get; set; }
        public long LastLoginTime { get; set; }
        public long CreateTime { get; set; }
    }
}