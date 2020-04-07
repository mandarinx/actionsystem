using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RL {

    public class AnimSystem {

        private readonly List<IEnumerator> animations = new List<IEnumerator>(1024);
        private readonly AnimSystemRunner  runner;

        public bool IsRunning => runner != null && runner.IsRunning;

        public AnimSystem() {
            runner = new GameObject("AnimSystemRunner", typeof(AnimSystemRunner))
               .GetComponent<AnimSystemRunner>();
        }

        public void Run() {
            runner.Run(animations);
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

    public class AnimSystemRunner : MonoBehaviour {

        public bool IsRunning { get; private set; }
        private readonly List<Coroutine> coroutines = new List<Coroutine>();
        
        public void Run(List<IEnumerator> animations) {
            IsRunning = true;
            StartCoroutine(RunAnimations(animations));
        }

        private IEnumerator RunAnimations(List<IEnumerator> animations) {
            coroutines.Clear();

            for (int i = 0; i < animations.Count; ++i) {
                coroutines.Add(StartCoroutine(animations[i]));
            }

            for (int i = 0; i < coroutines.Count; ++i) {
                yield return coroutines[i];
            }
            
            IsRunning = false;
        }
    }
}