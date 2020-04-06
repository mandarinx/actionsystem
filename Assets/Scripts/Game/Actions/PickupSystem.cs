using JetBrains.Annotations;
using UnityEngine;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(PickupAction), typeof(PropWeight))]
    public class PickupSystem : IActionSystem {

        public void Resolve(Item source, IAction action, Item target) {
            PropWeight propWeight = Property.Get<PropWeight>(target);
            
            int strength = 0;
            if (ItemDataSystem.Get(source, out ItemData itemData)) {
                strength = itemData.strength;
            }

            if (strength < propWeight.Weight) {
                Debug.Log($"{source.name} could not pick up {target.name}. It's too heavy!");
                return;
            }

            // Inventory.Remove(target.Owner.Items, target);
            Inventory.Add(source.Items, target);
            Item.Disable(target);
            Debug.Log($"{source.name} picked up {target.name}");
        }
    }
}
