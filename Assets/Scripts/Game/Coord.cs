using UnityEngine;

namespace RL {

    public class Coord {
        public Vector2Int map;
        public Vector3    world;
        public int        index;

        public Coord(Vector2Int mapCoord) {
            map   = mapCoord;
            world = Map.GetWorldCoord(map);
            index = Map.GetIndex(mapCoord.x, mapCoord.y);
        }

        public Coord(Vector3 worldPos) {
            map   = Map.GetMapCoord(worldPos, CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            world = worldPos;
            index = Map.GetIndex(map.x, map.y);
        }

        public Coord(int mapx, int mapy) {
            map   = new Vector2Int(mapx, mapy);
            world = Map.GetWorldCoord(map);
            index = Map.GetIndex(mapx, mapy);
        }

        public static void SetMapPosition(Coord mc, Vector2Int coord) {
            mc.map   = coord;
            mc.world = Map.GetWorldCoord(mc.map);
            mc.index = Map.GetIndex(mc.map.x, mc.map.y);
        }

        public static void SetWorldPosition(Coord mc, Vector3 worldPos) {
            mc.world = worldPos;
            mc.map   = Map.GetMapCoord(worldPos, CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            mc.index = Map.GetIndex(mc.map.x, mc.map.y);
        }

        public override string ToString() {
            return $"Coord {{map: {map}, world: {world}, index: {index}}}";
        }

        public override bool Equals(object obj) {
            Coord other = (Coord) obj;
            if (other == null) {
                return false;
            }
            return other.index == index;
        }

        public override int GetHashCode() {
            return index;
        }
    }
}