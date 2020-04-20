using UnityEngine;

namespace RL.Systems.Map {

    public static class TilingMethods {

        public static Sprite Autotile(Map map, int index, Group group, TilemapConfig tilemapConfig) {
            int tile = 0;
            if (map.HasTile(index + map.Width, group)) {
                tile |= Map.MASK_N;
            }
            if (map.HasTile(index + 1, group)) {
                tile |= Map.MASK_E;
            }
            if (map.HasTile(index - map.Width, group)) {
                tile |= Map.MASK_S;
            }
            if (map.HasTile(index - 1, group)) {
                tile |= Map.MASK_W;
            }
            return tilemapConfig.tileSprites[tile];
        }
    }
}