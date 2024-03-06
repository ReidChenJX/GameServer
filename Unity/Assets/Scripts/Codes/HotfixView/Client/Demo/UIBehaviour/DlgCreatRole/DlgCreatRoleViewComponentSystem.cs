
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgCreatRoleViewComponentAwakeSystem : AwakeSystem<DlgCreatRoleViewComponent> 
	{
		protected override void Awake(DlgCreatRoleViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgCreatRoleViewComponentDestroySystem : DestroySystem<DlgCreatRoleViewComponent> 
	{
		protected override void Destroy(DlgCreatRoleViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
