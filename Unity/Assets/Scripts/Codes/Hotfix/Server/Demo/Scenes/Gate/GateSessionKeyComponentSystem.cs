namespace ET.Server
{
    [FriendOf(typeof(GateSessionKeyComponent))]
    public static class GateSessionKeyComponentSystem
    {
        public static void Add(this GateSessionKeyComponent self, long key, long accountId)
        {
            self.sessionKey.Add(key, accountId);
            self.TimeoutRemoveKey(key).Coroutine();
        }

        public static long Get(this GateSessionKeyComponent self, long key)
        {
            long accountId = 0;
            self.sessionKey.TryGetValue(key, out accountId);
            return accountId;
        }

        public static void Remove(this GateSessionKeyComponent self, long key)
        {
            self.sessionKey.Remove(key);
        }

        private static async ETTask TimeoutRemoveKey(this GateSessionKeyComponent self, long key)
        {
            await TimerComponent.Instance.WaitAsync(20000);
            self.sessionKey.Remove(key);
        }
    }
}