
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgCreatRole))]
	[EnableMethod]
	public  class DlgCreatRoleViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_CommitButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CommitButton == null )
     			{
		    		this.m_E_CommitButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Sprite_BackGround/E_Commit");
     			}
     			return this.m_E_CommitButton;
     		}
     	}

		public UnityEngine.UI.Image E_CommitImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CommitImage == null )
     			{
		    		this.m_E_CommitImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_Commit");
     			}
     			return this.m_E_CommitImage;
     		}
     	}

		public UnityEngine.UI.InputField E_RoleNameInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleNameInputField == null )
     			{
		    		this.m_E_RoleNameInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"Sprite_BackGround/E_RoleName");
     			}
     			return this.m_E_RoleNameInputField;
     		}
     	}

		public UnityEngine.UI.Image E_RoleNameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleNameImage == null )
     			{
		    		this.m_E_RoleNameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Sprite_BackGround/E_RoleName");
     			}
     			return this.m_E_RoleNameImage;
     		}
     	}

		public UnityEngine.UI.Text E_ErrorTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ErrorTextText == null )
     			{
		    		this.m_E_ErrorTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Sprite_BackGround/E_ErrorText");
     			}
     			return this.m_E_ErrorTextText;
     		}
     	}

		public UnityEngine.UI.Text E_VersionText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_VersionText == null )
     			{
		    		this.m_E_VersionText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Sprite_BackGround/E_Version");
     			}
     			return this.m_E_VersionText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CommitButton = null;
			this.m_E_CommitImage = null;
			this.m_E_RoleNameInputField = null;
			this.m_E_RoleNameImage = null;
			this.m_E_ErrorTextText = null;
			this.m_E_VersionText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CommitButton = null;
		private UnityEngine.UI.Image m_E_CommitImage = null;
		private UnityEngine.UI.InputField m_E_RoleNameInputField = null;
		private UnityEngine.UI.Image m_E_RoleNameImage = null;
		private UnityEngine.UI.Text m_E_ErrorTextText = null;
		private UnityEngine.UI.Text m_E_VersionText = null;
		public Transform uiTransform = null;
	}
}
