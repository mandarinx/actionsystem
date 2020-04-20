using UnityEngine;

namespace RL.Systems.Map {

    public class MapSystem {
        
        // replace with an array or dictionary if multiple maps should be loaded
        // at once, like when chunking up a huge map
        private Map map;
        private MapRenderer renderer;

        public Map Map => map;
        
        public MapSystem(MapSystemConfig mapSysConfig, TilemapConfig[] tilemapConfigs) {
            // parse tilemap configs
            renderer = new MapRenderer(tilemapConfigs);
        }

        public void Load(int[] mapData, int mapWidth, string mapName) {
            // unload current map
            map = new Map(mapData, mapWidth);
            GameObject owner = new GameObject($"Map_{mapName}");
            renderer.Draw(map, owner);
        }
    }
}