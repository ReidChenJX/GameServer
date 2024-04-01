using System.Collections;
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
            self.View.E_LoginButton.AddListenerAsync(() => { return self.OnLoginClickHandler(); });
        }

        public static void ShowWindow(this DlgLogin self, Entity contextData = null)
        {
            
        }

        public static async ETTask OnLoginClickHandler(this DlgLogin self)
        {
            try
            {
                string accountText = self.View.E_AccountInputField.text.Trim();
                string passWdText = self.View.E_PasswordInputField.text.Trim();
                int errorCode =  await LoginHelper.Login(
                    self.ClientScene(), 
                    accountText, 
                    passWdText);
        
                if(errorCode != ErrorCode.ERR_Success)
                {
                    // 登录错误
                    Log.Error(errorCode.ToString());
                    //string errText = ShowLoginError(errorCode);
                    string errText = ErrorCodeSystem.ErrorExplain(errorCode);
                    self.View.E_ErrorTextText.text = errText;
                    return;
                }
                
                // 查询角色信息
                errorCode = await LoginHelper.GetRole(self.ClientScene());
                if(errorCode != ErrorCode.ERR_Success)
                {
                    // 无角色，进入角色创建页面
                    Log.Debug(errorCode.ToString());
                    // TODO 显示登录后的UI界面
                    self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
                    self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_CreatRole);
                    return;
                }
                
                // GetGate
                errorCode = await LoginHelper.GetGate(self.ClientScene());
                //await EnterMapHelper.EnterMapAsync(self.ClientScene());
                
                // TODO 显示登录后的UI界面
                self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Lobby);
                self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_UserName);
                // self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);
                
            }
            catch(Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}