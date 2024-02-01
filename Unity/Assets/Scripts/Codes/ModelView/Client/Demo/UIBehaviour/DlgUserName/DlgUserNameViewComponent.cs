
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgUserName))]
	[EnableMethod]
	public  class DlgUserNameViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_UserNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UserNameText == null )
     			{
		    		this.m_E_UserNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_UserName");
     			}
     			return this.m_E_UserNameText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_UserNameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_UserNameText = null;
		public Transform uiTransform = null;
	}
}
