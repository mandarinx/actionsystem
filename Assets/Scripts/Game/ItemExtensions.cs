using UnityEngine;

namespace RL {

    public static class ItemExtensions {

        public static IAction[] GetActions(this Item item) {
            return item.GetComponents<IAction>();
        }

        public static T GetAction<T>(this Item item) where T : Component, IAction {
            return item.GetComponent<T>();
        }

        public static bool HasAction<T>(this Item item) where T : Component, IAction {
            return item.GetComponent<T>() != null;
        }

        public static void AddAction<T>(this Item item) where T : Component, IAction {
            if (!HasAction<T>(item)) {
                item.gameObject.AddComponent<T>();
            }
        }

        public static void Enable(this Item item) {
            item.gameObject.SetActive(true);
        }

        public static void Disable(this Item item) {
            item.gameObject.SetActive(false);
        }

        public static void SetName(this Item item, string name) {
            item.gameObject.name = name;
        }

        public static void SetSprite(this Item item, Sprite sprite) {
            item.Visuals.sprite = sprite;
        }

        public static void SetSortingOrder(this Item item, int order) {
            item.Visuals.sortingOrder = order;
        }

        public static void SetLocalPosition(this Item item, Vector3 worldPos) {
            item.transform.localPosition = worldPos;
        }
    }
}