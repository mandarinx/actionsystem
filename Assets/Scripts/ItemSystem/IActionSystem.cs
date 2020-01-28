
using System;
using System.Collections;

namespace Altruist {

    public interface IActionSystem {
        Type TargetProperty { get; }
        IEnumerator Resolve(Item      source,
                            IAction   sourceAction,
                            Item      target,
                            IProperty targetProp);
    }
}