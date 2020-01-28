using UnityEngine;
using System.Collections.Generic;

namespace Altruist {

    public static class GO {

        public static GameObject Instantiate(Dictionary<string, GameObject> prefabs, string name) {
            GameObject instance = prefabs.TryGetValue(name, out GameObject prefab)
                ? GameObject.Instantiate(prefab)
                : null;
            if (instance != null) {
                instance.name = name;
            }
            return instance;

        }
    }
}