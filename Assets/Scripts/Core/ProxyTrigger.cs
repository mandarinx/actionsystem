using System.Collections.Generic;
using UnityEngine;

namespace Altruist {

    [RequireComponent(typeof(Collider))]
    public class ProxyTrigger : MonoBehaviour {

        private readonly List<ProxySignal> signals = new List<ProxySignal>();
        
        public void AddProxySignal(ProxySignal signal) {
            signals.Add(signal);
        }
        
        public void RemoveProxySignal(ProxySignal signal) {
            signals.Remove(signal);
        }
        
        private void OnTriggerEnter(Collider other) {
            for (int i = 0; i < signals.Count; ++i) {
                signals[i].SetEntered(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other) {
            for (int i = 0; i < signals.Count; ++i) {
                signals[i].SetExit(other.gameObject);
            }
        }

        private void OnTriggerStay(Collider other) {
            for (int i = 0; i < signals.Count; ++i) {
                signals[i].SetStay(other.gameObject);
            }
        }
    }
}