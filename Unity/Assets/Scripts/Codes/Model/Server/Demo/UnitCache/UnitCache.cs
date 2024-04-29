using System.Collections.Generic;

namespace ET.Server
{
    public interface IUnitCache
    {
        
    }
    
    public class UnitCache : Entity, IAwake, IDestroy
    {

        public string key { get; set; }
        public Dictionary<long, Entity> CacheComponentsDictionary { get; set; } = new Dictionary<long, Entity>();
    }
}

