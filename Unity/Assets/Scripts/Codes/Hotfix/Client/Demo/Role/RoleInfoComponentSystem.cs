using System;

namespace ET.Client
{
    public class RoleInfoComponentDestroySystem: DestroySystem<RoleInfoComponent>
    {
        protected override void Destroy(RoleInfoComponent self)
        {
            self.RoleInfo?.Dispose();
            self.CurrentRoleId = 0;
        }
    }
    
    
    
    
    
    [FriendOf(typeof(RoleInfoComponent))]
    public static class RoleInfoComponentSystem
    {
        
    }
}