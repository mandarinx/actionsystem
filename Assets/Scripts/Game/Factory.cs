using UnityEngine;
using Altruist;

namespace RL {

    public static class Factory {

        public static Item CreateItem(string name) {
            GameObject go = new GameObject();
            
            go.AddComponent<SpriteRenderer>();
            
            GameObject inventory = new GameObject("Items");
            inventory.transform.SetParent(go.transform, false);
            inventory.AddComponent<Inventory>();
            
            Item item = go.AddComponent<Item>();
            Item.SetName(item, name);
            ItemDataSystem.Set(item, new ItemData());
            
            PropPosition pos = Property.Add<PropPosition>(item);
            PropPosition.SetName(pos, name);
            
            return item;
        }
    }
}