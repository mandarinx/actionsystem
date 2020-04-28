using RL.Core;

namespace RL.Systems.Map {

    public class MapSystemConfig : ConfigBlock {
        public string name;
        public TilemapConfig[] tilemaps;

        public override string ToString() {
            return "MapSystemConfig {"+ 
                   $"name: {name}, "+ 
                   $"tilemaps: {tilemaps.Length}, "+ 
                   "}"
                ;
        }
    }
}