using UnityEngine;

namespace RL {

    public static class Factory {

        public static Item CreateItem(string name) {
            GameObject go = new GameObject();
            
            go.AddComponent<SpriteRenderer>();
            
            GameObject inventory = new GameObject("Items");
            inventory.transform.SetParent(go.transform, false);
            inventory.AddComponent<Inventory>();
            
            Item item = go.AddComponent<Item>();
            item.SetName(name);
            ItemDataSystem.Set(item, new ItemData());
            
            Property.Add<PropPosition>(item);
            
            return item;
        }
    }
}