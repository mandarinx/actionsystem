using UnityEngine;
using System.Collections;

namespace Altruist {

    public static class Action {

        public static IAction[] Get(Item item) {
            return item.GetComponents<IAction>();
        }

        public static IAction Get<T>(Item item) where T : Component, IAction {
            return item.GetComponent<T>();
        }

        public static bool Has<T>(Item item) where T : Component, IAction {
            return item.GetComponent<T>() != null;
        }

        public static void Add<T>(Item item) where T : Component, IAction {
            if (!Has<T>(item)) {
                item.gameObject.AddComponent<T>();
            }
        }
    }
}