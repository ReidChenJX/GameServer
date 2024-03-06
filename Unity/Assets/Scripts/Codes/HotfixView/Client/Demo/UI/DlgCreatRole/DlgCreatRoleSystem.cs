using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgCreatRole))]
	public static class DlgCreatRoleSystem
	{

		public static void RegisterUIEvent(this DlgCreatRole self)
		{
			self.View.E_CommitButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler(); });
		}

		public static void ShowWindow(this DlgCreatRole self, Entity contextData = null)
		{
			
		}


		public static async ETTask OnCreateRoleClickHandler(this DlgCreatRole self)
		{

			try
			{
				string roleName = self.View.E_RoleNameInputField.text.Trim();

				int errorCode = await LoginHelper.CreateRole(self.ClientScene(), roleName);

				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					string errText = ErrorCodeSystem.ErrorExplain(errorCode);
					self.View.E_ErrorTextText.text = errText;
					return;
				}
				
				
				await EnterMapHelper.EnterMapAsync(self.ClientScene());
				// TODO 显示登录后的UI界面
				self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_CreatRole);
				self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_UserName);
				
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			
			
			
			
			
			await ETTask.CompletedTask;
		}

	}
}
