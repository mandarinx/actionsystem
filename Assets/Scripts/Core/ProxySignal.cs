using UnityEngine;

namespace Altruist {

    public class ProxySignal {

        public bool       Entered    { get; private set; }
        public bool       Exit       { get; private set; }
        public bool       Stay       { get; private set; }
        public GameObject GameObject { get; private set; }

        public void SetEntered(GameObject go) {
            Entered = true;
            GameObject = go;
        }

        public void SetExit(GameObject go) {
            Exit = true;
            GameObject = go;
        }

        public void SetStay(GameObject go) {
            Stay = true;
            GameObject = go;
        }
    }
}