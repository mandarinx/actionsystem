using System;
using System.Collections.Generic;
using UnityEngine;

namespace RL {
    
    public class Map {

        private int width;
        // the data array represents a layer. Could be extended with
        // multiple arrays to support multiple layers
        private readonly int[] data;

        public int Width => width;
        public int Length => data.Length;

        public Map(int[] data, int width) {
            this.data = new int[data.Length];
            Array.Copy(data, this.data, data.Length);
            this.width = width;
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
