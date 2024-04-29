using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class UnitCacheComponent: Entity, IAwake, IDestroy
    {
        public List<string> UnitCacheKeyList { get; set; } = new List<string>();

        public Dictionary<string, UnitCache> UnitCaches { get; set; } = new Dictionary<string, UnitCache>();
    }
}