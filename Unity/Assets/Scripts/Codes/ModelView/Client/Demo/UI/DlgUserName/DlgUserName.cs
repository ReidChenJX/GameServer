namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgUserName :Entity,IAwake,IUILogic
	{

		public DlgUserNameViewComponent View { get => this.GetComponent<DlgUserNameViewComponent>();} 

		 

	}
}
