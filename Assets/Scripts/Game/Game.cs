using UnityEngine;

namespace RL {

    public class Game {

        public readonly Map        map;
        public readonly AnimSystem anim;

        private readonly Item      player;
        private readonly Item      selection;
        private readonly Assets    assets;
        
        private GameState state;
        
        private static Game self;
        
        public Game(Assets assets) {
            self = this;
            this.assets = assets;

            state = GameState.WARMUP;
            
            // Instantiate all systems necessary for other systems to function
            anim = new AnimSystem();
            map = new Map(CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            
            ActionSystem actionSys = new ActionSystem("RL");
            actionSys.OnActionSystemAdded += actionSystem => {
                IGameSystem gs = actionSys as IGameSystem;
                if (gs != null) {
                    gs.InitGame(this);
                }
            };
            actionSys.RegisterSystems();
            
            Map.CreateRoom(map, new Vector2Int(2, 2), 8, 6);
            MapRenderer.DrawLayer(map, CFG.LAYER_0, assets);

            player = Factory.CreateItem("Player");
            Item.SetSprite(player, Assets.GetEntity(assets, "Player"));
            Map.AddItem(map, player, new Coord(3, 3), CFG.LAYER_1);
            Action.Add<PickupAction>(player);
            Action.Add<PushAction>(player);
            ItemDataSystem.Set(player, new ItemData {
                strength = 10
            });

            Item sword = Factory.CreateItem("Sword");
            Item.SetSprite(sword, Assets.GetItem(assets, "Sword"));
            Property.Add<PropWeight>(sword);
            Map.AddItem(map, sword, new Coord(new Vector2Int(4, 3)), CFG.LAYER_1);

            Item crate = Factory.CreateItem("Crate");
            Item.SetSprite(crate, Assets.GetItem(assets, "Crate"));
            Map.AddItem(map, crate, new Coord(5, 4), CFG.LAYER_1);
            Property.Add<PropPushable>(crate);

            selection = Factory.CreateItem("Selection");
            Item.SetSprite(selection, Assets.GetMisc(assets, "Selection"));
            Coord selectionCoord = PositionSystem.Get("Selection", new Vector2Int(3, 3));
            Item.SetLocalPosition(selection, selectionCoord.world);
            Item.SetSortingOrder(selection, 100);

            Camera.main.transform.position = new Vector3(CFG.MAP_WIDTH  * 0.5f,
                                                         CFG.MAP_HEIGHT * 0.5f,
                                                         -10.0f);
        }

        public GameState Update(float dt) {

            switch (state) {
            case GameState.UNDEFINED:
            case GameState.WARMUP:
                RunSystems();
                state = GameState.AWAITING_PLAYER_INPUT;
            break;
            
            case GameState.AWAITING_PLAYER_INPUT:
                UpdateSelection();
                // get input command
                // if no command, wait
                // store input state
                // store input command
                // state = GameState.RESOLVE_ACTIONS;
            break;
            
            case GameState.RESOLVE_ACTIONS:
                // if left click mouse => walk
                // if left click mouse + shift => sneak
                // if right click mouse => resolve equipped actions with target item
                // get spent energy from energy system
                // make energy available for npcs
                // run npc ai => decide what to do, resolve actions
                // start animations
                Verbs.Main(this, player, PositionSystem.Get("Selection"));
                RunSystems();
                state = GameState.PLAY_ANIMATIONS;
            break;
            
            case GameState.PLAY_ANIMATIONS:
                // while animations are running => run systems
                // when done => switch state
                RunSystems();
                state = GameState.AWAITING_PLAYER_INPUT;
            break;
            }

            if (Property.Has<PropDead>(player)) {
                return GameState.GAME_OVER;
            }

            return state;
        }

        private void UpdateSelection() {
            Coord sc = PositionSystem.Get("Selection");
            Coord pc = PositionSystem.Get("Player");
            
            if (Input.GetKeyUp(KeyCode.UpArrow)
                && sc.map.y <= pc.map.y) {
                Coord.SetMapPosition(sc, pc.map + Vector2Int.up);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)
                && sc.map.y >= pc.map.y) {
                Coord.SetMapPosition(sc, pc.map + Vector2Int.down);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)
                && sc.map.x >= pc.map.x) {
                Coord.SetMapPosition(sc, pc.map + Vector2Int.left);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)
                && sc.map.x <= pc.map.x) {
                Coord.SetMapPosition(sc, pc.map + Vector2Int.right);
            }
            
            PositionSystem.Set("Selection", sc);
            Item.SetLocalPosition(selection, sc.world);
        }

        private void RunSystems() {
            // update visibility
            // action system player
            // update monster ai
            // action system monsters
            // play animations
            // map indexing?
        }
    }
}