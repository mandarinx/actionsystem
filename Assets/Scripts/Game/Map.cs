using UnityEngine;

namespace RL {
    
    public class Map {

        public readonly int[] tiles;

        public Map(int width, int height) {
            tiles = new int[width * height];
        }
        
        public static void CreateRoom(Map map, Vector2Int bottomLeft, int width, int height) {
            RectInt rect = GetRectangle(bottomLeft, width, height);
            // Debug.Log($"Rect | x:{rect.x} y:{rect.y} w:{rect.width} h:{rect.height}");
            FillRectangle(map, rect, CFG.TT_WALL);
            if (rect.width == 2
                || rect.height == 2) {
                return;
            }
            rect.x += 1;
            rect.y += 1;
            rect.width -= 2;
            rect.height -= 2;
            FillRectangle(map, rect, CFG.TT_FLOOR);
        }

        public static void FillRectangle(Map map, RectInt rect, int tile) {
            int len = rect.width * rect.height;
            // Debug.Log($"FillRect len: {len}");
            for (int i = 0; i < len; ++i) {
                Vector2Int coord = GetCoord(i).map;
                int index = GetIndex(rect.x + coord.x, rect.y + coord.y);
                // Debug.Log($"Coord: {coord} index: {index}");
                map.tiles[index] = tile;
            }
        }

        public static RectInt GetRectangle(Vector2Int bottomLeft, int width, int height) {
            int left = bottomLeft.x;
            left = Mathf.Clamp(left, 0, CFG.MAP_WIDTH);
            
            int right = left + width;
            right = Mathf.Clamp(right, 0, CFG.MAP_WIDTH);
            
            int bottom = bottomLeft.y;
            bottom = Mathf.Clamp(bottom, 0, CFG.MAP_HEIGHT);
            
            int top = bottom + height;
            top = Mathf.Clamp(top, 0, CFG.MAP_HEIGHT);
            
            return new RectInt(left, 
                               bottom, 
                               right - left, 
                               top - bottom);
        }

        public static int GetIndex(int x, int y) {
            return y * CFG.MAP_WIDTH + x;
        }

        public static int GetIndex(Vector2Int coord) {
            return GetIndex(coord.x, coord.y);
        }

        public static Vector2Int GetCoord(Vector3 worldPos) {
            return new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(worldPos.x), 0, CFG.MAP_WIDTH - 1), 
                                  Mathf.Clamp(Mathf.FloorToInt(worldPos.y), 0, CFG.MAP_HEIGHT - 1));
        }

        public static Coord GetCoord(int index) {
            return new Coord(new Vector2Int(index % CFG.MAP_WIDTH,
                                            Mathf.FloorToInt((float)index / CFG.MAP_WIDTH)));
        }

        public static Vector3 GetWorldCoord(Vector2Int mapCoord) {
            return new Vector3(mapCoord.x, mapCoord.y, 0f);
        }
        
        public static int GetTile(Map map, int i) {
            return map.tiles[i];
        }
    }
}
