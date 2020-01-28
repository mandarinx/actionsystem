using System.Collections.Generic;
using UnityEngine;

namespace AptGames {

    public interface IUpdate {
        void OnUpdate(float dt);
    }

    public class UnityUpdate : MonoBehaviour {

        private readonly List<IUpdate> updates = new List<IUpdate>(32768);
        private          int           count;
        private static   UnityUpdate   self;

        public static float dt;
        
        private void Awake() {
            self = this;
        }

        public static void Add(IUpdate handler) {
            if (self.updates.Contains(handler)) {
                return;
            }
            self.updates.Add(handler);
            self.count = self.updates.Count;
        }

        public static void Remove(IUpdate handler) {
            self.updates.Remove(handler);
            self.count = self.updates.Count;
        }

        private void Update() {
            dt = Time.deltaTime;
            for (int i = 0; i < count; ++i) {
                updates[i].OnUpdate(dt);
            }
        }
    }
}
