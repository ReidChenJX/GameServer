using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgUserName))]
    public static class DlgUserNameSystem
    {
        public static void RegisterUIEvent(this DlgUserName self)
        {
        }

        public static void ShowWindow(this DlgUserName self, Entity contextData = null)
        {
            var roleName = self.DomainScene().GetComponent<RoleInfoComponent>().RoleInfo.RoleName;
            self.View.E_UserNameText.text = "欢迎： " + roleName;
        }
    }
}