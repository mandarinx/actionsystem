using UnityEngine;

namespace RL {

    public class Coord {
        public Vector2Int map;
        public Vector3    world;
        public int        index;

        public Coord(Vector2Int mapCoord) {
            map   = mapCoord;
            world = Map.GetWorldCoord(map);
            index = Map.GetIndex(map);
        }

        public static void SetMapPosition(Coord mc, Vector2Int coord) {
            mc.map   = coord;
            mc.world = Map.GetWorldCoord(mc.map);
            mc.index = Map.GetIndex(mc.map);
        }

        public static void SetWorldPosition(Coord mc, Vector3 worldPos) {
            mc.world = worldPos;
            mc.map   = Map.GetCoord(worldPos, CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            mc.index = Map.GetIndex(mc.map);
        }

        public override string ToString() {
            return $"Coord {{map: {map}, world: {world}, index: {index}}}";
        }
    }
}