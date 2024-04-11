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

        public static async ETTask KickPlayer(Player player, bool isException = false)      // isException 是否异常下线
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

                if (!isException)
                {
                    switch (player.PlayerState)
                    {
                        case PlayerState.DisConnect:
                            break;
                        case PlayerState.Gate:
                            break;
                        case PlayerState.Game:
                            // 通知游戏服下线Unit 对象
                            M2G_RequestExitGame m2GRequestExitGame =
                                    (M2G_RequestExitGame)await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestExitGame());
                            
                            // 移除登录信息 LoginCenter
                            long LoginCenterConfigSceneId = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            L2G_RemoveLoginRecord g2LRemoveLoginRecord = (L2G_RemoveLoginRecord)await MessageHelper.CallActor(
                                LoginCenterConfigSceneId, new G2L_RemoveLoginRecord() { AccountId = player.AccountId, ZoneId = player.DomainZone() });
                            
                            break;
                    }
                }
                
                // 释放Player
                player.PlayerState = PlayerState.DisConnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.AccountId);
                player?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);
            }
        }
    }
}