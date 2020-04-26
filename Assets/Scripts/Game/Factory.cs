using RL.Core;
using RL.Systems.Items;
using UnityEngine;

namespace RL {

    public class Factory : IGameSystem {

        private Assets assets;
        
        public void Init(IGameSystems gameSystems, IConfig config, IAssets assets) {
            this.assets = (Assets)assets;
        }

        public Item CreatePlayer(string name) {
            GameObject go = new GameObject($"Player_{name}");
            
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = assets.GetEntity("Player");
            spriteRenderer.sortingOrder = 100;
            
            Inventory.Add(go.transform, "Items");
            
            Item item = go.AddComponent<Item>();
            item.SetName(name);
            
            return item;
        }
    }
}