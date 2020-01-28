using UnityEngine;
using System.Collections;

namespace AptGames {
    
    public class Coroutines : MonoBehaviour {

        private static Coroutines self;

        private void Awake() {
            self = this;
        }

        public static Coroutine Run(IEnumerator enumerator) {
            return self.StartCoroutine(enumerator);
        }

        public static void Stop(Coroutine coroutine) {
            self.StopCoroutine(coroutine);
        }
    }
}