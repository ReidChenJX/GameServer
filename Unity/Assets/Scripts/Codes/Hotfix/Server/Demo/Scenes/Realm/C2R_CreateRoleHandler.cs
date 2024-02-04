using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    public class C2R_CreateRoleHandler: AMRpcHandler<C2R_CreateRole, R2C_CreateRole>
    {
        // 消息处理类：客户端申请服务器创建角色
        protected override async ETTask Run(Session session, C2R_CreateRole request, R2C_CreateRole response)
        {
            // token 验证
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                session?.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_RoleNameNull;
                return;
            }

            if (session.GetComponent<SessionLockComponent>() != null)
            {
                // session 在第一次请求时挂载SessionLockComponent 组件，后续请求判断此时已挂载，则自动取消
                response.Error = ErrorCode.ERR_RequestRespeated;
                session.Disconnect().Coroutine();
                return;
            }

            // 同名查询
            using (session.AddComponent<SessionLockComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRoleLock, request.AccountId))
                {
                    RoleInfo roleInfo = null;
                    List<RoleInfo> roleInfosDB = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                            .Query<RoleInfo>(d => d.Name == request.Name && d.ServerId == request.ServerId);

                    if (roleInfosDB != null && roleInfosDB.Count > 0)
                    {
                        // 用户已注册，直接登录
                        // response.Error = ErrorCode.ERR_RoleNameSame;
                        // return;
                        roleInfo = roleInfosDB[0];
                        session.AddChild(roleInfosDB[0]);
                        response.RoleInfo = roleInfosDB[0].ToMessage();
                        roleInfo?.Dispose();
                        return;
                    }

                    // 新建角色，角色入库
                    roleInfo = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    roleInfo.Name = request.Name;
                    roleInfo.State = (int)RoleState.Normal;
                    roleInfo.ServerId = request.ServerId;
                    roleInfo.AccountId = request.AccountId;
                    roleInfo.CreateTime = TimeHelper.ServerNow();
                    roleInfo.LastLoginTime = 0;

                    await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<RoleInfo>(roleInfo);

                    // response proto 返回
                    response.RoleInfo = roleInfo.ToMessage();

                    roleInfo?.Dispose();
                }
            }
        }
    }
}