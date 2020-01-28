using UnityEngine;
using System.Collections.Generic;
using Altruist;

namespace RL {

    public class MapRenderer {

        private readonly Item[] tiles;

        public MapRenderer(int width, int height) {
            tiles = new Item[width * height];
            for (int i = 0; i < tiles.Length; ++i) {
                Coord coord = Map.GetCoord(i, CFG.MAP_WIDTH);
                tiles[i] = Factory.CreateItem($"{coord.map.x}_{coord.map.y}");
                Item.SetLocalPosition(tiles[i], coord.world);
            }
        }
        
        public static void DrawLayer(MapRenderer mr, Map map, Assets assets, int layer) {
            int len = mr.tiles.Length;
            for (int i = 0; i < len; ++i) {
                int tileType = map.tiles[i];

                switch (tileType) {
                case CFG.TT_FLOOR:
                    Item.SetSprite(mr.tiles[i], Assets.GetRandomFloor(assets));
                    break;
                
                case CFG.TT_WALL:
                    int flag = 0;
                    // North
                    if (i <= map.tiles.Length - CFG.MAP_WIDTH) {
                        if (map.tiles[i + CFG.MAP_WIDTH] == CFG.TT_WALL) {
                            flag |= CFG.FLAG_N;
                        }
                    }
                    // East
                    if (i % CFG.MAP_WIDTH < CFG.MAP_WIDTH - 1) {
                        if (map.tiles[i + 1] == CFG.TT_WALL) {
                            flag |= CFG.FLAG_E;
                        }
                    }
                    // South
                    if (i >= CFG.MAP_WIDTH) {
                        if (map.tiles[i - CFG.MAP_WIDTH] == CFG.TT_WALL) {
                            flag |= CFG.FLAG_S;
                        }
                    }
                    // West
                    if (i % CFG.MAP_WIDTH > 0) {
                        if (map.tiles[i - 1] == CFG.TT_WALL) {
                            flag |= CFG.FLAG_W;
                        }
                    }
                    
                    //Debug.Log($"Tile {i:00} flag: {Utils.PrintInt32(flag)}");
                    Item.SetSprite(mr.tiles[i], Assets.GetWall(assets, CFG.WALL_MAP[flag]));
                    break;

                default:
                    Item.SetSprite(mr.tiles[i], Assets.GetMisc(assets, "Blank"));
                    break;
                }

                Item.SetSortingOrder(mr.tiles[i], layer);
            }
        }

        public static void DrawPlayer(Dictionary<int, SpriteRenderer> sprites, int layer) {
            sprites[CFG.E_PLAYER].sortingOrder = layer;
        }

        public static Item GetTile(MapRenderer mr, int index) {
            return mr.tiles[index];
        }

        public static Item GetTile(MapRenderer mr, Coord coord) {
            return mr.tiles[coord.index];
        }
    }
}