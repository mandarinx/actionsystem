using System;
using RL.Systems.Map;

namespace RL {

    [Serializable]
    public class Config {

        public MapSystemConfig map;
        public TilemapConfig[] tilemaps;

        public static void Parse(Config config) {
            MapSystemConfig.Parse(config.map);
            for (int i = 0; i < config.tilemaps.Length; ++i) {
                TilemapConfig.Parse(config.tilemaps[i]);
            }
        }
    }
}