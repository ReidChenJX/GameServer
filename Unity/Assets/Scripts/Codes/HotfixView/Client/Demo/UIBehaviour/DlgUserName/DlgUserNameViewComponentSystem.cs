
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgUserNameViewComponentAwakeSystem : AwakeSystem<DlgUserNameViewComponent> 
	{
		protected override void Awake(DlgUserNameViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgUserNameViewComponentDestroySystem : DestroySystem<DlgUserNameViewComponent> 
	{
		protected override void Destroy(DlgUserNameViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
