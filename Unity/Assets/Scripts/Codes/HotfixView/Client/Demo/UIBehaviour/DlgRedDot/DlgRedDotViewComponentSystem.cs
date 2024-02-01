
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ObjectSystem]
	public class DlgRedDotViewComponentAwakeSystem : AwakeSystem<DlgRedDotViewComponent> 
	{
		protected override void Awake(DlgRedDotViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgRedDotViewComponentDestroySystem : DestroySystem<DlgRedDotViewComponent> 
	{
		protected override void Destroy(DlgRedDotViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
