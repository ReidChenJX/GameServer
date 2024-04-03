namespace ET.Server
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);

            if (self.InstanceId != instanceId)
            {
                return;
            }

            self.Dispose();
        }

        public static async ETTask KickPlayer(Player player)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            long instanceId = player.InstanceId;

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.AccountId.GetHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return;
                }

                switch (player.PlayerState)
                {
                    case PlayerState.DisConnect:
                        break;
                    case PlayerState.Gate:
                        break;
                    case PlayerState.Game:
                        // 执行Game Scene 的角色下线逻辑
                        
                        break;
                }

                player.PlayerState = PlayerState.DisConnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.AccountId);
                player?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);
            }
        }
    }
}