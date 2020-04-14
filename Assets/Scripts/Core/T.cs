using UnityEngine;

namespace RL.Core {

    public static class T {

        public static T Find<T>(Transform parent, string name) where T : class {
            Transform t = parent.Find(name);
            return t == null ? null : t.GetComponent<T>();
        }

        public static T[] FindAll<T>(Transform parent, string name) {
            Transform t = parent.Find(name);
            return t == null ? new T[0] : t.GetComponents<T>();
        }

        public static T AddChild<T>(string name, Transform parent, bool worldPositionStays = true) where T : Component {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, worldPositionStays);
            return go.AddComponent<T>();
        }

        public static GameObject AddChild(string name, Transform parent, bool worldPositionStays = true) {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, worldPositionStays);
            return go;
        }

    }
}