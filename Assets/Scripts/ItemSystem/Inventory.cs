using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Altruist {

    [AddComponentMenu("Item System/Inventory")]
    public class Inventory : MonoBehaviour {

        private readonly List<Item> contents = new List<Item>();
        private          Item       owner;
        private          int        max = int.MaxValue;

        private void OnEnable() {
            for (int i = 0; i < transform.childCount; ++i) {
                Item item = transform.GetChild(i).GetComponent<Item>();
                if (item != null) {
                    contents.Add(item);
                }
            }
            // Debug.Log($"Inventory {name} awake");
        }

        public static int Count(Inventory inventory) {
            return inventory.contents.Count;
        }

        public static void SetOwner(Inventory inventory, Item owner) {
            inventory.owner = owner;
        }

        public static Item GetOwner(Inventory inventory) {
            return inventory.owner;
        }

        public static void SetMax(Inventory inventory, int value) {
            inventory.max = value;
        }

        public static Item Get(Inventory inventory, int n) {
            return inventory.contents[n];
        }

        public static Item GetFirst(Inventory inventory) {
            return inventory.contents[0];
        }

        public static void Add(Inventory inventory, Item item) {
            if (inventory.contents.Count >= inventory.max) {
                return;
            }
            inventory.contents.Add(item);
            Item.SetOwner(item, inventory.owner);
            item.transform.SetParent(inventory.transform, false);
        }

        public static bool Remove(Inventory inventory, Item item) {
            return inventory.contents.Remove(item);
        }

        public static bool Replace(Inventory inventory, Item old, Item @new) {
            if (!Remove(inventory, old)) {
                return false;
            }
            Add(inventory, @new);
            return true;
        }
    }
}