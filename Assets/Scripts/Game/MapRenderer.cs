namespace RL {

    public static class MapRenderer {

        public static void DrawLayer(Map map, int layerIndex, Assets assets) {
            Item[] layer = Map.GetLayer(map, layerIndex);
            for (int i = 0; i < layer.Length; ++i) {
                Coord coord = Map.GetCoord(i, CFG.MAP_WIDTH);
                layer[i] = Factory.CreateItem($"{coord.map.x}_{coord.map.y}");
                layer[i].SetLocalPosition(coord.world);
                SetTile(layer, layerIndex, map.data, i, assets);
                layer[i].SetSortingOrder(layerIndex);
            }
        }
        
        private static void SetTile(Item[] layer, int layerIndex, int[] data, int dataIndex, Assets assets) {
            int tileType = data[dataIndex];

            switch (tileType) {
            case CFG.TT_FLOOR:
                layer[dataIndex].SetSprite(Assets.GetRandomFloor(assets));
                break;
            
            case CFG.TT_WALL:
                int flag = 0;
                // North
                if (dataIndex <= data.Length - CFG.MAP_WIDTH) {
                    if (data[dataIndex + CFG.MAP_WIDTH] == CFG.TT_WALL) {
                        flag |= CFG.FLAG_N;
                    }
                }
                // East
                if (dataIndex % CFG.MAP_WIDTH < CFG.MAP_WIDTH - 1) {
                    if (data[dataIndex + 1] == CFG.TT_WALL) {
                        flag |= CFG.FLAG_E;
                    }
                }
                // South
                if (dataIndex >= CFG.MAP_WIDTH) {
                    if (data[dataIndex - CFG.MAP_WIDTH] == CFG.TT_WALL) {
                        flag |= CFG.FLAG_S;
                    }
                }
                // West
                if (dataIndex % CFG.MAP_WIDTH > 0) {
                    if (data[dataIndex - 1] == CFG.TT_WALL) {
                        flag |= CFG.FLAG_W;
                    }
                }
                
                //Debug.Log($"Tile {dataIndex:00} flag: {Utils.PrintInt32(flag)}");
                layer[dataIndex].SetSprite(Assets.GetWall(assets, CFG.WALL_MAP[flag]));
                break;

            default:
                layer[dataIndex].SetSprite(Assets.GetMisc(assets, "Blank"));
                break;
            }
        }
    }
}