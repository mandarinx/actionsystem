using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Altruist {

    public class ActionRunner {
        public bool IsRunning { get; private set; }
        
        private readonly List<IEnumerator> enumerators     = new List<IEnumerator>();
        private readonly List<Coroutine>   runningRoutines = new List<Coroutine>();
        private readonly MonoBehaviour     owner;

        public static ActionRunner Create(string name) {
            MonoBehaviour owner = new GameObject(name, typeof(ActionRunnerOwner)).GetComponent<ActionRunnerOwner>();
            return new ActionRunner(owner);
        }

        private ActionRunner(MonoBehaviour owner) {
            this.owner = owner;
        }

        public void AddEnumerator(IEnumerator enumerator) {
            enumerators.Add(enumerator);
        }

        public void AddEnumerators(params IEnumerator[] enumerators) {
            this.enumerators.AddRange(enumerators);
        }

        public void Start() {
            owner.StartCoroutine(Run());
        }

        private IEnumerator Run() {
            IsRunning = true;
            runningRoutines.Clear();
            
            for (int i=0; i<enumerators.Count; ++i) {
                runningRoutines.Add(owner.StartCoroutine(enumerators[i]));
            }

            for (int i=0; i<runningRoutines.Count; ++i) {
                yield return runningRoutines[i];
            }
            
            enumerators.Clear();
            runningRoutines.Clear();
            IsRunning = false;
        }
    }
}