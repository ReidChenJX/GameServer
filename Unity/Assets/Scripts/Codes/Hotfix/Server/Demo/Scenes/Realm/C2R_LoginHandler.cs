using System;
using System.Net;

namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    public class C2R_LoginHandler: AMRpcHandler<C2R_Login, R2C_Login>
    {
        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
            // 登录名、密码格式验证放在前端进行
            // 锁定第一次验证
            if (session.GetComponent<SessionLockComponent>() != null)
            {
                // session 在第一次请求时挂载SessionLockComponent 组件，后续请求判断此时已挂载，则自动取消
                response.Error = ErrorCode.ERR_RequestRespeated;
                session.Disconnect().Coroutine();
                return;
            }

            // 链接通过，移出当前Session 上的自动销毁定时器
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            using (session.AddComponent<SessionLockComponent>())
            {
                // 数据库请求锁定
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountName.GetHashCode()))
                {
                    string passwdMD5 = MD5Helper.StringMD5(request.Password);
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<Account>(d => d.AccountName.Equals(request.AccountName));
                    Account account = null;
                    if (accountInfoList != null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountBlackListError;
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }

                        if (!account.PassWord.Equals(passwdMD5))
                        {
                            response.Error = ErrorCode.ERR_LoginInfoError;
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        Log.Debug("数据库无数据，自动创建。");
                        // Session 也是组件，新建的account 需要挂载在其下，用于记录，入库账户信息
                        account = session.AddChild<Account>();
                        account.AccountName = request.AccountName;
                        account.PassWord = passwdMD5;
                        account.CreateTime = TimeHelper.ServerNow();
                        account.AccountType = (int)AccountType.General;

                        // 数据入库
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);
                        Log.Debug("数据入库成功");
                    }
                    
                    // 验证该账户是否有其他设备正在登录
                    long accountSessionInstanceId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    Session otherSession = Root.Instance.Get(accountSessionInstanceId) as Session;
                    // 该账号已登录，由服务器向其他客户端发起下线
                    if (otherSession != null)
                    {
                        otherSession.Send(new R2C_Disconnect() { Error = ErrorCode.ERR_ExtraAccount });
                        otherSession.Disconnect().Coroutine();
                    }
                    
                    // 账户中心服务器验证
                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    var loginAccountRespone = (L2R_LoginAccountResponse)await ActorMessageSenderComponent.Instance.Call(
                        loginCenterInstanceId, new R2L_LoginAccountRequest() { AccountId = account.Id });

                    if (loginAccountRespone.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = ErrorCode.ERR_ExtraLoginCenter;
                        session.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }
                    
                    
                    
                }

                await ETTask.CompletedTask;

                // // 随机分配一个Gate
                // StartSceneConfig config = RealmGateAddressHelper.GetGate(session.DomainZone());
                // Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
                //
                // // 向gate请求一个key,客户端可以拿着这个key连接gate
                // G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey) await ActorMessageSenderComponent.Instance.Call(
                // 	config.InstanceId, new R2G_GetLoginKey() {Account = request.Account});
                //
                // response.Address = config.InnerIPOutPort.ToString();
                // response.Key = g2RGetLoginKey.Key;
                // response.GateId = g2RGetLoginKey.GateId;
            }
        }
    }
}