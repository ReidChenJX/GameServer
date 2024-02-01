﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgLogin))]
    public static  class DlgLoginSystem
    {

        public static void RegisterUIEvent(this DlgLogin self)
        {
            // self.View.E_LoginButton.AddListenerAsync(() => { return self.OnLoginClickHandler(); });
        }

        public static void ShowWindow(this DlgLogin self, Entity contextData = null)
        {

			
        }

        // public static async ETTask OnLoginClickHandler(this DlgLogin self)
        // {
        //     try
        //     {
        //         string accountText = self.View.E_AccountInputField.text.Trim();
        //         string passWdText = self.View.E_PasswordInputField.text.Trim();
        //         int errorCode =  await LoginHelper.LoginAccount(
        //             self.ClientScene(), 
        //             accountText, 
        //             passWdText);
        //
        //         if(errorCode != ErrorCode.ERR_Success)
        //         {
        //             // 登录错误
        //             Log.Error(errorCode.ToString());
        //             string errText = ShowLoginError(errorCode);
        //             self.View.E_LoginErrorTextText.text = errText;
        //             return;
        //         }
        //         
        //         // 获取区服信息
        //         errorCode = await LoginHelper.GetServerInfos(self.ClientScene());
        //         // 手动设置选择区服
        //         self.DomainScene().GetComponent<ServerInfoComponent>().CurrentServerId = 1;
        //         // 手动创建角色信息 角色名 = 登录名
        //         errorCode = await LoginHelper.CreateRole(self.ClientScene(), accountText);
        //         
        //         // EnterMap 
        //         errorCode = await LoginHelper.EnterGame(self.ClientScene());
        //         await EnterMapHelper.EnterMapAsync(self.ClientScene());
        //         
        //         Log.Debug("HideWindow(WindowID.WindowID_Login)");
        //         // TODO 显示登录后的UI界面
        //         self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
        //         self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_UserName);
        //         // self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);
        //         
        //     }
        //     catch(Exception e)
        //     {
        //         Log.Error(e.ToString());
        //     }
        // }

        public static string ShowLoginError(int errCode)
        {
            switch (errCode)
            {
                case 200003 :
                    return "账户名、密码错误！";
                case 200004:
                    return "密码格式错误，需包含数字，大小写字母，长度为6-15之间！";
                case 200005:
                    return "账户处于黑名单！";
            }
            return "";
        }
    }
}