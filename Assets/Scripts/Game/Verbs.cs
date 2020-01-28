using Altruist;

namespace RL {

    public static class Verbs {

        public static void Main(Item player, Item floorTile) {
        
            if (Inventory.Count(floorTile.Items) > 0) {
                PickupAction pickupAction = Action.Get(player).Find<PickupAction>();
                if (pickupAction == null) {
                    return;
                }
                
                Item item = Inventory.GetFirst(floorTile.Items);
                ActionSystem.Resolve(player, pickupAction, item);
            }
                
            // check map for entities at floorCoord
                // > fight
            
            MoveAction moveAction = Action.Get(player).Find<MoveAction>();
            ActionSystem.Resolve(player, moveAction, floorTile);

            // Could make a MoveAction that targets a PositionProp.
            // Anything that occupies a position has a PositionProp.
            // The PositionProp could communicate with a centralized system for more efficiently
            // handling indexing of positions.
            // Rendering Items would need to get all coordinates from the central system,
            // then get all the corresponding Items, and update their transform.
            // PositionProp would only be a bridge from an Item into the positioning system, and
            // the position data could be read/write. That would make it possible for Items to
            // push each other. An Item can be pushed into a wall, unless something prevents it.
            // Could use a property change event on all Items? 
        }
    }
}