﻿using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class GateSessionKeyComponent : Entity, IAwake
    {
        public readonly Dictionary<long, long> sessionKey = new Dictionary<long, long>();
    }
}