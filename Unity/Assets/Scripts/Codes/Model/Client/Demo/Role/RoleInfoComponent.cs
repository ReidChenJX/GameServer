using System.Collections.Generic;
using System.Net.Sockets;

namespace ET.Client
{
    [ComponentOf]
    public class RoleInfoComponent: Entity, IAwake, IDestroy
    {
        public RoleInfo RoleInfo { get; set; }
        public long CurrentRoleId { get; set; } = 0;

    }
}