
namespace RL {

    public class MapSystem {

        // replace with an array or dictionary if multiple maps should be loaded
        // at once, like when chunking up a huge map
        private Map map;
        private MapRenderer renderer;

        public Map Map => map;
        
        public MapSystem(MapConfig mapConfig, TilemapConfig[] tilemapConfigs) {
            // parse tilemap configs
            //renderer = new MapRenderer(parsedTilemapConfigs);
        }

        public void Load(int[] mapData, int mapWidth) {
            // unload current map
            map = new Map(mapData, mapWidth);
            // renderer.Draw();
        }
    }
}