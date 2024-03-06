using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof (ErrorCode))]
    public static class ErrorCodeSystem
    {
        public static string ErrorExplain(int errCode)
        {
            switch (errCode)
            {
                case 200002 :
                    return "网络错误，请检查网络信息";
                case 200003 :
                    return "账户名、密码错误！";
                case 200004:
                    return "密码格式错误，需包含数字，大小写字母，长度为6-15之间！";
                case 200005:
                    return "账户处于黑名单！";
                case 200007 :
                    return "该用户已登录，正在下线中。";
                case 200008 :
                    return "该用户已登录，正在下线中。";
                case 200009 :
                    return "验证失败";
                case 200010 :
                    return "请输入用户名。";
                case 200011 :
                    return "该用户名已存在。";
                case 200012 :
                    return "请输入用户名。";
                case 200100 :
                    return "未知错误，请联系管理员";
                case 300000 :
                    return "资源初始化失败。";
                case 300001 :
                    return "资源更新版本号失败";
                case 300002 :
                    return "资源更新清单失败";
                case 300003 :
                    return "资源更新下载失败";
                
            }
            return "";
        }
    }

}
