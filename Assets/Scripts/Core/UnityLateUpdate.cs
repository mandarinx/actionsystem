using System.Collections.Generic;
using UnityEngine;

namespace AptGames.Unity {

    public interface ILateUpdate {
        void OnLateUpdate(float dt);
    }

    public class UnityLateUpdate : MonoBehaviour {

        private readonly List<ILateUpdate> lateUpdates = new List<ILateUpdate>(32768);
        private          int               count;
        private static   UnityLateUpdate   self;

        private void Awake() {
            self = this;
        }

        public static void Add(ILateUpdate handler) {
            self.lateUpdates.Remove(handler);
            self.lateUpdates.Add(handler);
            self.count = self.lateUpdates.Count;
        }

        public static void Remove(ILateUpdate handler) {
            self.lateUpdates.Remove(handler);
            self.count = self.lateUpdates.Count;
        }

        private void LateUpdate() {
            float dt = Time.deltaTime;
            for (int i = 0; i < count; ++i) {
                lateUpdates[i].OnLateUpdate(dt);
            }
        }
    }
}
