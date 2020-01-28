using System;
using System.Collections;
using UnityEngine;
using Altruist;
using JetBrains.Annotations;
using Action = Altruist.Action;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(PushAction))]
    public class PushSystem : IActionSystem {

        public Type TargetProperty => typeof(PropPushable);

        public IEnumerator Resolve(Item source, IAction sourceAction, Item target, Bridge bridge) {
            
            // get direction
            // check if map coordinate at target pos + direction is free
            // if it isn't, cancel the move
            // if it is, move both source and target

            PropPosition posSource = Property.Get<PropPosition>(source);
            PropPosition posTarget = Property.Get<PropPosition>(target);

            Coord coordSource = PropPosition.GetCoord(posSource);
            Coord coordTarget = PropPosition.GetCoord(posTarget);
            
            Vector2Int dir = coordTarget.map - coordSource.map;
            Vector2Int pushTargetMapCoord = coordTarget.map + dir;

            Coord pushTargetCoord = new Coord(pushTargetMapCoord);
            
            Map map = bridge.Get<Map>();
            if (!Map.IsWalkable(map, pushTargetCoord)) {
                yield break;
            }

            ActionRunner runner = bridge.Get<ActionRunner>();
            ActionSystem.Resolve(source, Action.Get<MoveAction>(source), target, runner);
            ActionSystem.Resolve(target, Action.Get<MoveAction>(target), Map.GetItem(map, pushTargetCoord, CFG.LAYER_0), runner);
        }
    }
}
