
using System;

namespace RL {

    public interface IActionSystem {
        Type TargetProperty { get; }
        void Resolve(Item    source,
                     IAction action,
                     Item    target);
    }
}