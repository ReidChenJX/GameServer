using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    public class C2R_GetRoleHandler: AMRpcHandler<C2R_GetRole, R2C_GetRole>
    {
        // 消息处理类：客户端申请服务器查询角色
        protected override async ETTask Run(Session session, C2R_GetRole request, R2C_GetRole response)
        {
            // token 验证
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                session?.Disconnect().Coroutine();
                return;
            }
            
            if (session.GetComponent<SessionLockComponent>() != null)
            {
                // session 在第一次请求时挂载SessionLockComponent 组件，后续请求判断此时已挂载，则自动取消
                response.Error = ErrorCode.ERR_RequestRespeated;
                session.Disconnect().Coroutine();
                return;
            }
            
            // 角色查询
            using (session.AddComponent<SessionLockComponent>())
            {
                RoleInfo roleInfo = null;
                List<RoleInfo> roleInfosDB = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                        .Query<RoleInfo>(d => d.AccountId == request.AccountId);

                if (roleInfosDB != null && roleInfosDB.Count > 0)
                {
                    // 存在角色
                    roleInfo = roleInfosDB[0];
                    session.AddChild(roleInfosDB[0]);
                    response.RoleInfo = roleInfosDB[0].ToMessage();
                    roleInfo?.Dispose();
                    return;
                }
                else
                {
                    // 不存在，返回ERR_RoleEmpty
                    response.Error = ErrorCode.ERR_RoleEmpty;
                    roleInfo?.Dispose();
                }
            }
        }
    }
}