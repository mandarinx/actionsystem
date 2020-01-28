using System;
using System.Collections;
using Altruist;
using JetBrains.Annotations;
using UnityEngine;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(OpenAction))]
    public class OpenSystem : IActionSystem {

        public Type TargetProperty => typeof(PropLock);

        public IEnumerator Resolve(Item source, IAction sourceAction, Item target, IProperty targetProp) {
            yield break;
        }
    }
}
