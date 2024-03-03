namespace ET.Client
{
    public class AccountInfoComponentDestroySystem : DestroySystem<AccountInfoComponent>
    {
        protected override void Destroy(AccountInfoComponent self)
        {
            self.Token = string.Empty;
            self.AccountId = 0;
            self.LoginName = string.Empty;
            self.Key = string.Empty;

        }
    }

    [FriendOf(typeof(AccountInfoComponent))]
    public static class  AccountInfoComponetSystem
    {

    }
}