using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene clientScene, string account, string password)
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

                IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

                // 登录验证
                Session session = await RouterHelper.CreateRouterSession(clientScene, realmAddress);
                R2C_Login r2CLogin = (R2C_Login)await session.Call(new C2R_Login() { AccountName = account, Password = password });

                if (r2CLogin.Error != ErrorCode.ERR_Success)
                {
                    return r2CLogin.Error;
                }

                clientScene.GetComponent<SessionComponent>().Session = session;

                clientScene.GetComponent<AccountInfoComponent>().Token = r2CLogin.Token;
                clientScene.GetComponent<AccountInfoComponent>().AccountId = r2CLogin.AccountId;
                clientScene.GetComponent<AccountInfoComponent>().Name = account;

                // 登录返回
                if (r2CLogin.Error != ErrorCode.ERR_Success)
                {
                    return r2CLogin.Error;
                }

                // 创建一个gate Session,并且保存到SessionComponent中
                // Session gateSession = await RouterHelper.CreateRouterSession(clientScene, NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                // clientScene.AddComponent<SessionComponent>().Session = gateSession;
                //
                // G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
                //     new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});
                //
                // Log.Debug("登陆gate成功!");

                await EventSystem.Instance.PublishAsync(clientScene, new EventType.LoginFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_OERR;
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetServerInfos(Scene clientScene)
        {
            try
            {
                // Client 向 Realm 申请区服
                R2C_GetServerInfo r2CGetServerInfo = (R2C_GetServerInfo)await clientScene.GetComponent<SessionComponent>().Session.Call(
                    new C2R_GetServerInfo()
                    {
                        AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
                        Token = clientScene.GetComponent<AccountInfoComponent>().Token
                    });

                if (r2CGetServerInfo.Error != ErrorCode.ERR_Success)
                {
                    return r2CGetServerInfo.Error;
                }

                foreach (var serverPorto in r2CGetServerInfo.ServerInfoList)
                {
                    ServerInfo serverInfo = clientScene.GetComponent<ServerInfoComponent>().AddChild<ServerInfo>();
                    serverInfo.FromMessage(serverPorto);
                    clientScene.GetComponent<ServerInfoComponent>().Add(serverInfo);
                }
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_OERR;
            }
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRole(Scene clientScene)
        {
            try
            {
                R2C_CreateRole r2CCreateRole = (R2C_CreateRole)await clientScene.DomainScene().GetComponent<SessionComponent>().Session.Call(
                    new C2R_CreateRole()
                    {
                        AccountId = clientScene.GetComponent<AccountInfoComponent>().AccountId,
                        Token = clientScene.GetComponent<AccountInfoComponent>().Token,
                        Name = clientScene.GetComponent<AccountInfoComponent>().Name,
                        ServerId = clientScene.GetComponent<ServerInfoComponent>().CurrentServerId
                    });
                
                
                
                
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_OERR;
            }
            
        
            await ETTask.CompletedTask;
            return ErrorCode.ERR_OERR;
        }
    }
}