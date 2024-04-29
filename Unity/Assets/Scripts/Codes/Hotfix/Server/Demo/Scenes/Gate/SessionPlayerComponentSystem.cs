

namespace ET.Server
{
	public static class SessionPlayerComponentSystem
	{
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			protected override void Destroy(SessionPlayerComponent self)
			{
				// 玩家下线计时操作
				if (!self.isLoginAgain && self.PlayerInstanceId != 0)
				{
					Player player = self.GetMyPlayer();
					PlayerOfflineOutTimeComponent playerOfflineOutTimeComponent = player.GetComponent<PlayerOfflineOutTimeComponent>();
					if (playerOfflineOutTimeComponent == null)
					{
						player.AddComponent<PlayerOfflineOutTimeComponent>();
					}
				}

				self.PlayerId = 0;
				self.PlayerInstanceId = 0;
				self.isLoginAgain = false;
			}
		}

		public static Player GetMyPlayer(this SessionPlayerComponent self)
		{
			return self.DomainScene().GetComponent<PlayerComponent>().Get(self.PlayerId);
		}
	}
}
