using System;
using RL.Core;
using RL.Systems.Map;

namespace RL {

    [Serializable]
    public class Config : IConfig {

        public MapSystemConfig mapSystem;

        public static void Parse(Config config) {
            MapSystemConfig.Parse(config.mapSystem);
        }
    }
}