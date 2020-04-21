using JetBrains.Annotations;
using RL.Core;
using RL.Systems.Game;
using RL.Systems.Items;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(PushAction), typeof(PropPushable))]
    public class PushSystem : IActionSystem, IGameSystem {

        private AnimSystem anim;
        // private Map map;
        

        public void Init(IGameSystems gameSystems, IConfig config, IAssets assets) {
            // anim = game.anim;
            // map = game.map;
        }

        public void Resolve(Item source, IAction action, Item target) {
            
            // get direction
            // check if map coordinate at target pos + direction is free
            // if it isn't, cancel the move
            // if it is, move both source and target

            PropPosition posSource = Property.Get<PropPosition>(source);
            PropPosition posTarget = Property.Get<PropPosition>(target);

            Coord coordSource = PropPosition.GetCoord(posSource);
            Coord coordTarget = PropPosition.GetCoord(posTarget);
            
            // Vector2Int dir = coordTarget.map - coordSource.map;
            // Vector2Int pushTargetMapCoord = coordTarget.map + dir;

            // Coord pushTargetCoord = new Coord(pushTargetMapCoord);

            // if (!Map.IsWalkable(map, pushTargetCoord)) {
                // return;
            // }
            
            anim.DoMove(source.transform, 
                        source.transform.position, 
                        target.transform.position);
            // anim.DoMove(target.transform, 
                        // target.transform.position, 
                        // Map.GetItem(map, pushTargetCoord, CFG.LAYER_0).transform.position);
            // ActionSystem.Resolve(source, Action.Get<MoveAction>(source), target);
            // ActionSystem.Resolve(target, Action.Get<MoveAction>(target), Map.GetItem(map, pushTargetCoord, CFG.LAYER_0));
        }
    }
}
