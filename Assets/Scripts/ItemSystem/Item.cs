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
        public Item Owner { get; internal set; }

        private void Awake() {
            Items      = Find<Inventory>(transform, "Items");
            Skills     = Find<Inventory>(transform, "Skills");
            Visuals    = GetComponent<SpriteRenderer>();

            if (Skills != null) {
                Inventory.SetOwner(Skills, this);
            }

            if (Items != null) {
                Inventory.SetOwner(Items, this);
            }
        }

        private static T Find<T>(Transform parent, string name) where T : class {
            Transform t = parent.Find(name);
            return t == null ? null : t.GetComponent<T>();
        }
    }
}