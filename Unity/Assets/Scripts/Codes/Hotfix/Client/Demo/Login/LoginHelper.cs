using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene clientScene, string loginName, string password)
        {
            try
            {
                // 创建一个ETModel层的Session
                clientScene.RemoveComponent<RouterAddressComponent>();
                clientScene.RemoveComponent<NetClientComponent>();
                // 获取路由跟realmDispatcher地址
                RouterAddressComponent routerAddressComponent = clientScene.GetComponent<RouterAddressComponent>();
                if (routerAddressComponent == null)
                {
                    routerAddressComponent =
                            clientScene.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
                    await routerAddressComponent.Init();

                    clientScene.AddComponent<NetClientComponent, AddressFamily>(routerAddressComponent.RouterManagerIPAddress.AddressFamily);
                }

                // 获取第一个Zone 的Realm 地址
                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress();

                // 登录验证
                Session Realmsession = await RouterHelper.CreateRouterSession(clientScene, realmAddress);
                R2C_Login r2CLogin = (R2C_Login)await Realmsession.Call(new C2R_Login() { LoginName = loginName, Password = password });

                if (r2CLogin.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(r2CLogin.Error.ToString());
                    return r2CLogin.Error;
                }

                clientScene.GetComponent<SessionComponent>().Session = Realmsession;

                clientScene.GetComponent<AccountInfoComponent>().Token = r2CLogin.Token;
                clientScene.GetComponent<AccountInfoComponent>().AccountId = r2CLogin.AccountId;
                clientScene.GetComponent<AccountInfoComponent>().LoginName = loginName;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_OERR;
            }

            return ErrorCode.ERR_Success;
        }

        // public static async ETTask<int> GetServerInfos(Scene clientScene)
        // {
        //     try
        //     {
        //         // Client 向 Realm 申请区服
        //         R2C_GetServerInfo r2CGetServerInfo = (R2C_GetServerInfo)await clientScene.GetComponent<SessionComponent>().Session.Call(
        //             new C2R_GetServerInfo()
        //             {
        //                 AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
        //                 Token = clientScene.GetComponent<AccountInfoComponent>().Token
        //             });
        //
        //         if (r2CGetServerInfo.Error != ErrorCode.ERR_Success)
        //         {
        //             return r2CGetServerInfo.Error;
        //         }
        //
        //         foreach (var serverPorto in r2CGetServerInfo.ServerInfoList)
        //         {
        //             ServerInfo serverInfo = clientScene.GetComponent<ServerInfoComponent>().AddChild<ServerInfo>();
        //             serverInfo.FromMessage(serverPorto);
        //             clientScene.GetComponent<ServerInfoComponent>().Add(serverInfo);
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         Log.Debug(e.ToString());
        //         return ErrorCode.ERR_OERR;
        //     }
        //     await ETTask.CompletedTask;
        //     return ErrorCode.ERR_Success;
        // }

        public static async ETTask<int> GetRole(Scene clientScene)
        {
            try
            {
                R2C_GetRole r2CGetRole = (R2C_GetRole)await clientScene.DomainScene().GetComponent<SessionComponent>().Session.Call(
                    new C2R_GetRole()
                    {
                        AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
                        Token = clientScene.GetComponent<AccountInfoComponent>().Token
                    });
                
                if (r2CGetRole.Error != ErrorCode.ERR_Success)
                {
                    Log.Debug($"该登录用户无角色信息");
                    Log.Error(r2CGetRole.Error.ToString());
                    return r2CGetRole.Error;
                }
                
                // 记录 角色选择信息
                RoleInfo newRoleInfo = clientScene.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
                newRoleInfo.FromMessage(r2CGetRole.RoleInfo);

                clientScene.GetComponent<RoleInfoComponent>().RoleInfo = newRoleInfo;
                clientScene.GetComponent<RoleInfoComponent>().CurrentRoleId = newRoleInfo.Id;
                Log.Debug($"当前获取的角色{newRoleInfo.Id}");
                
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_OERR;
            }
            
            return ErrorCode.ERR_Success;
        }
        
        public static async ETTask<int> CreateRole(Scene clientScene, string roleName)
        {
            try
            {
                R2C_CreateRole r2CCreateRole = (R2C_CreateRole)await clientScene.DomainScene().GetComponent<SessionComponent>().Session.Call(
                    new C2R_CreateRole()
                    {
                        AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
                        Token = clientScene.GetComponent<AccountInfoComponent>().Token,
                        RoleName = roleName
                    });
                
                if (r2CCreateRole.Error != ErrorCode.ERR_Success)
                {
                    Log.Error(r2CCreateRole.Error.ToString());
                    return r2CCreateRole.Error;
                }
                
                // 记录 角色选择信息
                RoleInfo newRoleInfo = clientScene.GetComponent<RoleInfoComponent>().AddChild<RoleInfo>();
                newRoleInfo.FromMessage(r2CCreateRole.RoleInfo);

                clientScene.GetComponent<RoleInfoComponent>().RoleInfo = newRoleInfo;
                clientScene.GetComponent<RoleInfoComponent>().CurrentRoleId = newRoleInfo.Id;
                Log.Debug($"当前创建并获取的角色{newRoleInfo.Id}");
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_OERR;
            }
            
            return ErrorCode.ERR_Success;
        }


        public static async ETTask<int> GetGate(Scene clientScene)
        {
            try
            {
                R2C_GetGate r2CLoginGate = (R2C_GetGate)await clientScene.DomainScene().GetComponent<SessionComponent>().Session.Call(
                    new C2R_GetGate()
                    {
                        AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
                        Token = clientScene.GetComponent<AccountInfoComponent>().Token
                    });
                
                // 创建一个gate Session,并且保存到SessionComponent中
                Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLoginGate.Address));

                clientScene.GetComponent<SessionComponent>().Session.Disconnect().Coroutine();
                clientScene.GetComponent<SessionComponent>().Session = gateSession;
            
                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                    new C2G_LoginGate() { Key = r2CLoginGate.Key, GateId = r2CLoginGate.GateId});
            
                Log.Debug("登陆gate成功!");
                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginFinish());
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_OERR;
            }
            
            return ErrorCode.ERR_Success;
        }
    }


    public static class LoginStep
    {
        public static async ETTask LoginStepAll()
        {
            // 登录过程
            // int errorCode =  await LoginHelper.LoginAccount(self.ClientScene(), accountText, passWdText);
            // 向 Realm 服务器申请验证登录名与密码，返回 AccountId Token
            
            // 角色获取
            // int errorCode =  await LoginHelper.GetRole(self.ClientScene());
            
            
            
            
            
            await ETTask.CompletedTask;
        }
    }
    
}