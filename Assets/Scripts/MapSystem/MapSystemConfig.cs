using System;

namespace RL.Systems.Map {

    [Serializable]
    public class MapSystemConfig {
        
        public TilemapConfig[] tilemaps;

        public static void Parse(MapSystemConfig mapSystemConfig) {
            for (int i = 0; i < mapSystemConfig.tilemaps.Length; ++i) {
                TilemapConfig.Parse(mapSystemConfig.tilemaps[i]);
            }
        }
    }
}