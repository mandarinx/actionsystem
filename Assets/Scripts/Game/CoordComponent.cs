using UnityEngine;

namespace RL {

    public class CoordComponent : MonoBehaviour {

        [SerializeField] private Vector2Int mapCoord;
        [SerializeField] private int        mapIndex;

        public Coord Value => new Coord(mapIndex, mapCoord, transform.position);

        public static void SetMapIndex(CoordComponent cc, Vector2Int coord) {
            cc.mapCoord           = coord;
            // cc.transform.position = Map.GetWorldCoord(cc.mapCoord);
            // cc.mapIndex           = Map.GetIndex(cc.mapCoord.x, cc.mapCoord.y);
        }

        public static void SetMapPosition(CoordComponent cc, Vector2Int coord) {
            cc.mapCoord           = coord;
            // cc.transform.position = Map.GetWorldCoord(cc.mapCoord);
            // cc.mapIndex           = Map.GetIndex(cc.mapCoord.x, cc.mapCoord.y);
        }

        public static void SetWorldPosition(CoordComponent cc, Vector3 worldPos) {
            cc.transform.position = worldPos;
            // cc.mapCoord           = Map.GetMapCoord(worldPos, CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            // cc.mapIndex           = Map.GetIndex(cc.mapCoord.x, cc.mapCoord.y);
        }
    }
}