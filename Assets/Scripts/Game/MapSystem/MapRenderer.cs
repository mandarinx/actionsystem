using System.Collections.Generic;
using UnityEngine;

namespace RL {

    public class MapRenderer {

        // accept parsed tilemap configs
        public MapRenderer() {
            
        }

        // accept map
        public void Draw() {
            // iterate tiles, do stuff
        }

        public void AutoTile(Map                     map,
                             Dictionary<int, Sprite> tilemap,
                             Dictionary<int, Sprite> tiles) {

            for (int i = 0; i < map.Length; ++i) {
                // get tile data at i
                // if tile type is wall
                // > look for neighbours of same type at NESW
                // > calculate wall index
                // if tile type is floor, water, grass ...
                // > pick a random sprite
                
                // get tile data
                // get tile group from data
                // lookup a tilemap from the dictionary using the tile group as key
                // get neighbours of same type
                // calculate tile value from neighbours
                // pass tile value to tilemap to get a sprite
                // add sprite to tiles dictionary at tile index
            }
        }
        // public static void DrawLayer(Map map, int layerIndex, Assets assets) {
        //     Item[] layer = Map.GetLayer(map, layerIndex);
        //     for (int i = 0; i < layer.Length; ++i) {
        //         Coord coord = Map.GetCoord(i, CFG.MAP_WIDTH);
        //         layer[i] = Factory.CreateItem($"{coord.map.x}_{coord.map.y}");
        //         layer[i].SetLocalPosition(coord.world);
        //         SetTile(layer, layerIndex, map.data, i, assets);
        //         layer[i].SetSortingOrder(layerIndex);
        //     }
        // }
        //
        // private static void SetTile(Item[] layer, int layerIndex, int[] data, int dataIndex, Assets assets) {
        //     int tileType = data[dataIndex];
        //
        //     switch (tileType) {
        //     case CFG.TT_FLOOR:
        //         layer[dataIndex].SetSprite(Assets.GetRandomFloor(assets));
        //         break;
        //     
        //     case CFG.TT_WALL:
        //         int flag = 0;
        //         // North
        //         if (dataIndex <= data.Length - CFG.MAP_WIDTH) {
        //             if (data[dataIndex + CFG.MAP_WIDTH] == CFG.TT_WALL) {
        //                 flag |= CFG.FLAG_N;
        //             }
        //         }
        //         // East
        //         if (dataIndex % CFG.MAP_WIDTH < CFG.MAP_WIDTH - 1) {
        //             if (data[dataIndex + 1] == CFG.TT_WALL) {
        //                 flag |= CFG.FLAG_E;
        //             }
        //         }
        //         // South
        //         if (dataIndex >= CFG.MAP_WIDTH) {
        //             if (data[dataIndex - CFG.MAP_WIDTH] == CFG.TT_WALL) {
        //                 flag |= CFG.FLAG_S;
        //             }
        //         }
        //         // West
        //         if (dataIndex % CFG.MAP_WIDTH > 0) {
        //             if (data[dataIndex - 1] == CFG.TT_WALL) {
        //                 flag |= CFG.FLAG_W;
        //             }
        //         }
        //         
        //         //Debug.Log($"Tile {dataIndex:00} flag: {Utils.PrintInt32(flag)}");
        //         layer[dataIndex].SetSprite(Assets.GetWall(assets, CFG.WALL_MAP[flag]));
        //         break;
        //
        //     default:
        //         layer[dataIndex].SetSprite(Assets.GetMisc(assets, "Blank"));
        //         break;
        //     }
        // }
    }
}