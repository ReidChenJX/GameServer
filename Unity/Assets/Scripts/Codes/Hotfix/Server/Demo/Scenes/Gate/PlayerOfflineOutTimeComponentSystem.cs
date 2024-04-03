using System;
using System.Timers;

namespace ET.Server
{
    [Invoke(TimerInvokeType.PlayerOfflineOutTime)]
    public class PlayerOfflineOutTime: ATimer<PlayerOfflineOutTimeComponent>
    {
        protected override void Run(PlayerOfflineOutTimeComponent self)
        {
            try
            {
                self.KickPlayer();
            }
            catch (Exception e)
            {
                Log.Error($"用户下线倒计时错误： {self.Id} \n {e}");
            }
        }
    }

    [FriendOf(typeof (PlayerOfflineOutTimeComponent))]
    public class PlayerOfflineOutTimeComponentAwakeSystem: AwakeSystem<PlayerOfflineOutTimeComponent>
    {
        protected override void Awake(PlayerOfflineOutTimeComponent self)
        {
            self.Timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 10000, TimerInvokeType.PlayerOfflineOutTime, self);
        }
    }

    public class PlayerOfflineOutTimeComponentDestroySystem: DestroySystem<PlayerOfflineOutTimeComponent>
    {
        protected override void Destroy(PlayerOfflineOutTimeComponent self)
        {
            TimerComponent.Instance.Remove(ref self.Timer);
        }
    }

    public static class GateUnitDeleteComponentSystem
    {
        public static void KickPlayer(this PlayerOfflineOutTimeComponent self)
        {
            DisconnectHelper.KickPlayer(self.GetParent<Player>()).Coroutine();
        }
    }
}