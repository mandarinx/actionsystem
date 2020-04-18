using System;

namespace RL {

    [Serializable]
    public class Config {

        public MapConfig       map;
        public TilemapConfig[] tilemaps;

        public static void Parse(Config config) {
            MapConfig.Parse(config.map);
            for (int i = 0; i < config.tilemaps.Length; ++i) {
                TilemapConfig.Parse(config.tilemaps[i]);
            }
        }
    }
}