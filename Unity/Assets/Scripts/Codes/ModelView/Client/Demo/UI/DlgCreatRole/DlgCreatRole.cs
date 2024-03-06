namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCreatRole :Entity,IAwake,IUILogic
	{

		public DlgCreatRoleViewComponent View { get => this.GetComponent<DlgCreatRoleViewComponent>();} 

		 

	}
}
