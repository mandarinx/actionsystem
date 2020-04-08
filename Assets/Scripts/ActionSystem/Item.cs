using Altruist.Core;
using UnityEngine;

namespace RL {

    [AddComponentMenu("Item System/Item")]
    public class Item : MonoBehaviour {

        public Inventory      Skills     { get; private set; }
        public Inventory      Items      { get; private set; }
        public SpriteRenderer Visuals    { get; private set; }

        // When an Item is instantiated, it has no owner.
        // When it gets added to an Inventory, it will be owned
        // by the owner of the Inventory. An Inventory is always
        // owned by an Item, so any Items in an Inventory is owned
        // by a survivor, skill or whatever the Item represents.
        public Item Owner { get; private set; }

        private void Awake() {
            Items      = T.Find<Inventory>(transform, "Items");
            Skills     = T.Find<Inventory>(transform, "Skills");
            Visuals    = GetComponent<SpriteRenderer>();

            if (Skills != null) {
                Inventory.SetOwner(Skills, this);
            }

            if (Items != null) {
                Inventory.SetOwner(Items, this);
            }
        }

        public static void Enable(Item item) {
            item.gameObject.SetActive(true);
        }

        public static void Disable(Item item) {
            item.gameObject.SetActive(false);
        }

        public static void SetOwner(Item item, Item owner) {
            item.Owner = owner;
        }

        public static void SetName(Item item, string name) {
            item.gameObject.name = name;
        }

        public static void SetSprite(Item item, Sprite sprite) {
            item.Visuals.sprite = sprite;
        }

        public static void SetSortingOrder(Item item, int order) {
            item.Visuals.sortingOrder = order;
        }

        public static void SetLocalPosition(Item item, Vector3 worldPos) {
            item.transform.localPosition = worldPos;
        }
    }
}