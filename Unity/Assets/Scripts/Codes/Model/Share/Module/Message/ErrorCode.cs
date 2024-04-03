namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常
        
        public const int ERR_NetWorkError = 200002;             // 网络错误
        public const int ERR_LoginInfoError = 200003;           // 登录信息错误
        public const int ERR_PasswordFormError = 200004;        // 密码格式错误
        public const int ERR_AccountBlackListError = 200005;    // 账户处于黑名单
        public const int ERR_RequestRespeated = 200006;         // 多次登录
        public const int ERR_ExtraAccount = 200007;             // 其他人登录，挤下线
        public const int ERR_ExtraLoginCenter = 200008;         // 账户中心服务器已存在账号
        public const int ERR_TokenError = 200009;               // Token 验证错误
        
        // Role 错误
        public const int ERR_RoleNameNull = 200010;             // Role Name 字段为空
        public const int ERR_RoleNameSame = 200011;             // Role 重复
        public const int ERR_RoleEmpty = 200012;                // Role 为空
        
        // 未定义错误
        public const int ERR_OERR = 200100;       
        
        // 300000 - 310000 客户端框架异常
        public const int ERR_ResourceInitError = 300000;            // 资源初始化失败
        public const int ERR_ResourceUpdateVersionError = 300001;   // 资源更新版本号失败
        public const int ERR_ResourceUpdateManifestError = 300002;  // 资源更新清单失败
        public const int ERR_ResourceUpdateDownloadError = 300003;  // 资源更新下载失败
    }
}