namespace ET.Server
{
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<long>
    {
        public long AccountId { get; set; }
		
        public long UnitId { get; set; }
    }
}