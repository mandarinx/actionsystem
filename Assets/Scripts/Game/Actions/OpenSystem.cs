using JetBrains.Annotations;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(OpenAction), typeof(PropLock))]
    public class OpenSystem : IActionSystem {

        public void Resolve(Item source, IAction action, Item target) {
        }
    }
}
