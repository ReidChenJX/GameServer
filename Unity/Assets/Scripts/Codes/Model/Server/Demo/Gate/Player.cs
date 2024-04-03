namespace ET.Server
{
    public enum PlayerState
    {
        DisConnect,
        Gate,
        Game,
    }
    
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<long>
    {
        public long AccountId { get; set; }
        
        public long SessionInstanceId { get; set; }
		
        public long UnitId { get; set; }
        
        public PlayerState PlayerState { get; set; }
    }

    public class PlayerSystem: AwakeSystem<Player, long>
    {
        protected override void Awake(Player self, long a)
        {
            self.AccountId = a;
        }
    }
}