namespace ET.Server
{
    [FriendOf(typeof(Player))]
    public static class PlayerSystem
    {
        [ObjectSystem]
        public class PlayerAwakeSystem : AwakeSystem<Player, long>
        {
            protected override void Awake(Player self, long a)
            {
                self.AccountId = a;
            }
        }
    }
}