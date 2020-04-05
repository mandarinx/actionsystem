using System;
using JetBrains.Annotations;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(OpenAction))]
    public class OpenSystem : IActionSystem {

        public Type TargetProperty => typeof(PropLock);

        public void Resolve(Item source, IAction action, Item target) {
        }
    }
}
