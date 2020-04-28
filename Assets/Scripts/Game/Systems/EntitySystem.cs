using RL.Core;
using RL.Systems.Items;
using UnityEngine;

namespace RL.Systems.Game {

    public class EntitySystem : IGameSystem {

        private Assets assets;
        private int lastUID;

        public void Init(IGameSystems gameSystems, Context ctx) {
            assets = (Assets)ctx.assets;
        }
        
        // accept position parameter
        public Item CreatePlayer(string name) {
            int uid = GetUID();
            GameObject go = new GameObject($"Player_{name}_{uid}");
            
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite       = assets.GetSprite("entities/player");
            spriteRenderer.sortingOrder = 100;
            
            Inventory.Add(go.transform, "Items");
            
            Item item = go.AddComponent<Item>();
            item.SetName(name);
            
            // dispatch message MEntityCreated { uid, position }
            return item;
        }

        private int GetUID() {
            lastUID += 1;
            return lastUID;
        }
    }
}