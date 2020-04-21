using System;
using UnityEngine;

namespace RL.Systems.Map {
    
    public class Map {

        public const int MASK_GROUP = 0xFF; // 255, 8 bits
        public const int MASK_THEME = 0xFF; // 255, 8 bits
        public const int MASK_SPAWNPOINT = 0x10000; // the 17th bit

        public const int MASK_N = 1 << 3;
        public const int MASK_E = 1 << 2;
        public const int MASK_S = 1 << 1;
        public const int MASK_W = 1 << 0;

        private readonly int width;
        private readonly int height;
        // the data array represents a layer. Could be extended with
        // multiple arrays to support multiple layers
        private readonly int[] data;

        public int Width => width;
        public int Length => data.Length;
        public int Spawnpoint { get; }

        public Map(int[] data, int width) {
            this.data = new int[data.Length];
            Array.Copy(data, this.data, data.Length);
            this.width = width;
            height = Mathf.FloorToInt(this.data.Length / (float)width);
            Debug.Log($"width: {width} height: {height}");
            
            // search for spawn point, and other useful meta data
            for (int i = 0; i < this.data.Length; ++i) {
                if (HasSpawnpoint(i)) {
                    Spawnpoint = i;
                    return;
                }
            }
        }

        public bool HasSpawnpoint(int tileIndex) {
            return (data[tileIndex] & MASK_SPAWNPOINT) > 0;
        }

        public Vector3 IndexToWorldPos(int tileIndex) {
            Vector2Int coord = IndexToCoord(tileIndex);
            // multiply by tile size if needed
            return new Vector3(coord.x, coord.y, 0f);
        }

        public Vector2Int IndexToCoord(int tileIndex) {
            int y = Mathf.FloorToInt(tileIndex / (float)width);
            int x = tileIndex % width;
            return new Vector2Int(x, y);
        }

        public Vector3 CoordToWorldPos(Vector2Int coord) {
            // multiply by tile size if needed
            return new Vector3(coord.x, coord.y, 0f);
        }

        public int CoordToIndex(Vector2Int coord) {
            return coord.y * width + coord.x;
        }

        public Vector2Int WorldPosToCoord(Vector3 worldPos) {
            int x = Mathf.FloorToInt(worldPos.x);
            x = Mathf.Clamp(x, 0, width - 1);
            int y = Mathf.FloorToInt(worldPos.y);
            y = Mathf.Clamp(y, 0, height - 1);
            return new Vector2Int(x, y);
        }

        public int WorldPosToIndex(Vector3 worldPos) {
            int x = Mathf.FloorToInt(worldPos.x);
            x = Mathf.Clamp(x, 0, width - 1);
            int y = Mathf.FloorToInt(worldPos.y);
            y = Mathf.Clamp(y, 0, height - 1);
            return y * width + x;
        }

        public bool TryGetTile(int i, out int tile) {
            bool valid = i >= 0 && i < Length;
            tile = valid ? data[i] : -1;
            return valid;
        }

        public bool TryGetTile(int i, Group group, out int tile) {
            if (!HasTile(i, group)) {
                tile = -1;
                return false;
            }
            
            tile = data[i];
            return true;
        }

        public bool HasTile(int i, Group group) {
            return (i >= 0 && i < Length) 
                   && GetGroup(data[i]) == group;
        }

        public static Group GetGroup(int data) {
            return (Group)(data & MASK_GROUP);
        }
        
        // methods for converting world pos to tile pos, tile index
        
        // private readonly Dictionary<int, Item[]> layers = new Dictionary<int, Item[]>();
        //
        // public Map(int width, int height) {
        //     data = new int[width * height];
        //     AddLayer(layers, 0, width, height);
        //     AddLayer(layers, 1, width, height);
        // }
        //
        // private static void AddLayer(Dictionary<int, Item[]> layerTable, int layerNum, int width, int height) {
        //     layerTable.Add(layerNum, new Item[width * height]);
        // }
        //
        // public static Item[] GetLayer(Map map, int layer) {
        //     return map.layers[layer];
        // }
        //
        // public static void CreateRoom(Map map, Vector2Int bottomLeft, int width, int height) {
        //     RectInt rect = GetRectangle(bottomLeft, width, height);
        //     // Debug.Log($"Rect | x:{rect.x} y:{rect.y} w:{rect.width} h:{rect.height}");
        //     FillRectangle(map, rect, CFG.TT_WALL);
        //     if (rect.width == 2
        //         || rect.height == 2) {
        //         return;
        //     }
        //     rect.x += 1;
        //     rect.y += 1;
        //     rect.width -= 2;
        //     rect.height -= 2;
        //     FillRectangle(map, rect, CFG.TT_FLOOR);
        // }
        //
        // public static void FillRectangle(Map map, RectInt rect, int tile) {
        //     int len = rect.width * rect.height;
        //     // Debug.Log($"FillRect len: {len}");
        //     for (int i = 0; i < len; ++i) {
        //         Vector2Int coord = GetCoord(i, rect.width).map;
        //         int index = GetIndex(rect.x + coord.x, rect.y + coord.y);
        //         // Debug.Log($"Coord: {coord} index: {index}");
        //         map.data[index] = tile;
        //     }
        // }
        //
        // public static RectInt GetRectangle(Vector2Int bottomLeft, int width, int height) {
        //     int left = bottomLeft.x;
        //     left = Mathf.Clamp(left, 0, CFG.MAP_WIDTH);
        //     
        //     int right = left + width;
        //     right = Mathf.Clamp(right, 0, CFG.MAP_WIDTH);
        //     
        //     int bottom = bottomLeft.y;
        //     bottom = Mathf.Clamp(bottom, 0, CFG.MAP_HEIGHT);
        //     
        //     int top = bottom + height;
        //     top = Mathf.Clamp(top, 0, CFG.MAP_HEIGHT);
        //     
        //     return new RectInt(left, 
        //                        bottom, 
        //                        right - left, 
        //                        top - bottom);
        // }
        //
        // public static Item GetItem(Map map, Coord coord, int layer) {
        //     return map.layers[layer][coord.index];
        // }
        //
        // public static void AddItem(Map map, Item item, Coord coord, int layer) {
        //     Item other = GetItem(map, coord, layer);
        //     if (other != null) {
        //         Debug.Log($"Cannot add Item {item.name} to layer {layer} at coord {coord} because Item {other.name} is already there");
        //         return;
        //     }
        //     map.layers[layer][coord.index] = item;
        //     item.SetSortingOrder(layer);
        //     PositionSystem.Set(item, coord);
        //     item.SetLocalPosition(coord.world);
        // }
        //
        // public static int GetData(Map map, Coord coord) {
        //     return map.data[coord.index];
        // }
        //
        // public static int GetIndex(int x, int y) {
        //     return y * CFG.MAP_WIDTH + x;
        // }
        //
        // public static int GetIndex(Coord coord) {
        //     return GetIndex(coord.map.x, coord.map.y);
        // }
        //
        // public static Coord GetCoord(Vector3 worldPos, int width, int height) {
        //     return new Coord(GetMapCoord(worldPos, width, height));
        // }
        //
        // public static Vector2Int GetMapCoord(Vector3 worldPos, int width, int height) {
        //     return new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(worldPos.x), 0, width - 1),
        //                           Mathf.Clamp(Mathf.FloorToInt(worldPos.y), 0, height - 1));
        // }
        //
        // public static Coord GetCoord(int index, int width) {
        //     return new Coord(new Vector2Int(index % width,
        //                                     Mathf.FloorToInt((float)index / width)));
        // }
        //
        // public static Vector3 GetWorldCoord(Vector2Int mapCoord) {
        //     return new Vector3(mapCoord.x, mapCoord.y, 0f);
        // }
        //
        // public static bool IsWalkable(Map map, Coord coord) {
        //     if (GetItem(map, coord, CFG.LAYER_1)) {
        //         return false;
        //     }
        //     return map.data[coord.index] == CFG.TT_FLOOR;
        // }
    }
}
