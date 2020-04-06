using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RL {

    // register an update handler to be able to pick up when anims are done
    
    public class AnimSystem {

        private          List<IEnumerator> animations = new List<IEnumerator>(1024);
        private readonly AnimSystemRunner  runner;
        
        public bool IsRunning { get; }
        
        public AnimSystem() {
            runner = new GameObject("AnimSystemRunner", typeof(AnimSystemRunner))
               .GetComponent<AnimSystemRunner>();
        }

        public void Run() {
            // IsRunning = true;
            // UnityUpdate.Add(this);
            // runner.Run();
        }

        public void OnUpdate(float dt) {
            // if (!runner.IsRunning) {
            // UnityUpdate.Remove(this);
            // IsRunning = false;
            // }
        }

        public void DoMove(Transform transform, Vector3 fromPos, Vector3 toPos) {
            animations.Add(Move(transform, fromPos, toPos));
        }

        private static IEnumerator Move(Transform transform, Vector3 fromPos, Vector3 toPos) {
            Vector3 direction = toPos - fromPos;
            float   dist   = direction.magnitude;
            float   travel = 0f;

            while (travel < dist) {
                Vector3 pos = fromPos + direction * travel;
                transform.position = pos;
                travel += Time.deltaTime * CFG.E_SPEED;
                yield return null;
            }

            transform.position = toPos;
        }
    }

    public class AnimSystemRunner : MonoBehaviour {}
}