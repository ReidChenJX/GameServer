using System.Collections.Generic;

namespace ET.Server
{
    // AccountId 与 当前角色所处的 Scene.Zone 区服信息
    [ComponentOf(typeof(Scene))]
    public class LoginInfoRecordComponent : Entity, IAwake, IDestroy
    {
        // <accountId  ZoneID>
        public Dictionary<long, int> AccountLoginInfoDict = new Dictionary<long, int>();
    }
}