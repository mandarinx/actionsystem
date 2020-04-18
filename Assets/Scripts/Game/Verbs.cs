using UnityEngine;

namespace RL {

    public static class Verbs {

        public static void Main(Game game, Item player, Coord selCoord) {

            if (!Input.GetKeyUp(KeyCode.Space)) {
                return;
            }

            // Is there an Item on the floor?
            // Item target = Map.GetItem(game.map, selCoord, CFG.LAYER_1);
            // if (target != null) {
            //  
            //     Debug.Log("there is something on the floor");
            //     
            //     // Try to pick it up
            //     if (Do<PickupAction>(player, target)) {
            //         Debug.Log("its possible to pick up");
            //         return;
            //     }
            //
            //     // Try to push it
            //     if (Do<PushAction>(player, target)) {
            //         Debug.Log("it can be pushed");
            //         return;
            //     }
            // }

            // Get the floor tile
            // target = Map.GetItem(game.map, selCoord, CFG.LAYER_0);
            // Debug.Log($"Tile {target.name} is {(Map.IsWalkable(game.map, selCoord) ? "walkable" : "blocked")}");
            //
            // if (Map.IsWalkable(game.map, selCoord)) {
            //     game.anim.DoMove(player.transform, 
            //                      player.transform.position,
            //                      target.transform.position);
            //     // MoveAction moveAction = Action.Get(player).Find<MoveAction>();
            //     // ActionSystem.Resolve(player, moveAction, target);
            //     return;
            // }
            
            // else if player has the means to break through a non-walkable tile
            
            // if tile is water, change to SwimAction.
            // > Swim could toggle a state machine that consumes energy
            // > for each turn the player is in water.
        }

        private static bool Do<T>(Item source, Item target) where T : Component, IAction {
            T action = source.GetAction<T>();
            return action != null && ActionSystem.Resolve(source, action, target);
        }
    }
}