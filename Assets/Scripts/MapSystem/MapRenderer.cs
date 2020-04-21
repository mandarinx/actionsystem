using System.Collections.Generic;
using UnityEngine;

namespace RL.Systems.Map {

    public class MapRenderer {

        private readonly Dictionary<Group, TilemapConfig> tilemapConfigs;
        
        public MapRenderer(TilemapConfig[] tilemapConfigs) {
            this.tilemapConfigs = new Dictionary<Group, TilemapConfig>();
            for (int i = 0; i < tilemapConfigs.Length; ++i) {
                this.tilemapConfigs.Add(tilemapConfigs[i].group, tilemapConfigs[i]);
            }
        }

        public void Draw(Map map, GameObject owner) {
            for (int i = 0; i < map.Length; ++i) {
                if (!map.TryGetTile(i, out int tile)) {
                    continue;
                }

                Group group = Map.GetGroup(tile);
                TilemapConfig tilemapConfig = tilemapConfigs[group];
                Sprite sprite;
                
                switch (tilemapConfig.tilingMethod) {
                    case TilingMethod.AUTOTILE:
                        sprite = TilingMethods.Autotile(map, i, group, tilemapConfig);
                        break;
                    default:
                        sprite = tilemapConfigs[Group.Misc].tileSprites[0];
                        break;
                }

                Vector2Int coord = map.IndexToCoord(i);
                GameObject go = new GameObject($"Tile_{coord.x:00}_{coord.y:00}",
                                               typeof(SpriteRenderer));
                go.transform.SetParent(owner.transform, worldPositionStays: false);
                go.transform.position = map.IndexToWorldPos(i);
                SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingOrder = -1000;
            }
        }
    }
}