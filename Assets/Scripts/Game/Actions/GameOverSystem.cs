using System;
using System.Collections;
using Altruist;
using JetBrains.Annotations;
using UnityEngine;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(GameOverAction))]
    public class GameOverSystem : IActionSystem {

        public Type TargetProperty => typeof(PropVoid);

        public IEnumerator Resolve(Item source, IAction sourceAction, Item target, IProperty targetProp) {
            Debug.Log("GAME OVER");
            yield break;
        }
    }
}
