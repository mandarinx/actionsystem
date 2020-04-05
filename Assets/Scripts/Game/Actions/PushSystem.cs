using System;
using UnityEngine;
using JetBrains.Annotations;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(PushAction))]
    public class PushSystem : IActionSystem {

        public Type TargetProperty => typeof(PropPushable);

        public void Resolve(Item source, IAction action, Item target) {
            
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

            Map map = Game.Cur.map;
            if (!Map.IsWalkable(map, pushTargetCoord)) {
                return;
            }
            
            ActionSystem.Resolve(source, Action.Get<MoveAction>(source), target);
            ActionSystem.Resolve(target, Action.Get<MoveAction>(target), Map.GetItem(map, pushTargetCoord, CFG.LAYER_0));
        }
    }
}
