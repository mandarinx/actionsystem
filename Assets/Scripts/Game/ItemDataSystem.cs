using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Altruist;

namespace RL {

    public static class ItemDataSystem {
        
        private static readonly Dictionary<int, ItemData> itemDatas = new Dictionary<int, ItemData>();

        public static bool Get(Item item, out ItemData itemData) {
            return itemDatas.TryGetValue(item.GetInstanceID(), out itemData);
        }

        public static void Set(Item item, ItemData itemData) {
            itemDatas[item.GetInstanceID()] = itemData;
        }
    }
}