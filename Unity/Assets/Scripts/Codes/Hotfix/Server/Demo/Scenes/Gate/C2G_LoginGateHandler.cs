using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response)
		{
			Scene scene = session.DomainScene();
			
			// 锁定第一次验证
			if (session.GetComponent<SessionLockComponent>() != null)
			{
				// session 在第一次请求时挂载SessionLockComponent 组件，后续请求判断此时已挂载，则自动取消
				response.Error = ErrorCode.ERR_RequestRespeated;
				session.Disconnect().Coroutine();
				return;
			}
			
			long accountId = scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
			if (accountId == 0)
			{
				response.Error = ErrorCore.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				return;
			}
			
			// 验证成功，移除记录的key与对应的AccountId
			scene.GetComponent<GateSessionKeyComponent>().Remove(request.Key);
			session.RemoveComponent<SessionAcceptTimeoutComponent>();

			long instanceId = session.InstanceId;
			
			using (session.AddComponent<SessionLockComponent>())
			using(await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, accountId.GetHashCode()))
			{
				if (instanceId != session.InstanceId)
				{
					return;
				}
				
				
				PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
				Player player = playerComponent.AddChild<Player, long>(accountId);
				playerComponent.Add(player);
				session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
				session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);

				response.PlayerId = player.Id;
				await ETTask.CompletedTask;
			}

			
		}
	}
}