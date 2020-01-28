using System;
using System.Collections;
using Altruist;
using JetBrains.Annotations;
using UnityEngine;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(PickupAction))]
    public class PickupSystem : IActionSystem {

        public Type TargetProperty => typeof(PropWeight);

        public IEnumerator Resolve(Item source, IAction sourceAction, Item target, Bridge bridge) {
            PropWeight propWeight = Property.Get<PropWeight>(target);
            
            int strength = 0;
            if (ItemDataSystem.Get(source, out ItemData itemData)) {
                strength = itemData.strength;
            }

            if (strength < propWeight.Weight) {
                Debug.Log($"{source.name} could not pick up {target.name}. It's too heavy!");
                yield break;
            }

            // Inventory.Remove(target.Owner.Items, target);
            Inventory.Add(source.Items, target);
            Item.Disable(target);
            Debug.Log($"{source.name} picked up {target.name}");
        }
    }
}
