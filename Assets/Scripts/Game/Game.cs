using System;
using UnityEngine;

namespace RL {

    public enum InputCommand {
        UNDEFINED = 0,
        WALK      = 10,
        INTERACT  = 1000,
    }

    [Flags]
    public enum Dir {
        NONE = 0,
        N    = 1 << 0,
        E    = 1 << 1,
        S    = 1 << 2,
        W    = 1 << 3,
    }

    [Flags]
    public enum InputKey {
        NONE      = 0,
        PRIMARY   = 1 << 0,
        SECONDARY = 1 << 1,
    }

    public struct InputState {
        public Vector3  mousePos;
        public InputKey key;
        public Dir      direction;
    }
    
    public class Game {

        public readonly Map            map;
        public readonly AnimSystem     anim;
        public readonly MovementSystem move;

        private readonly Item      player;
        private readonly Item      selection;
        private readonly Assets    assets;
        
        private GameState state;
        private InputCommand inputCmd;
        
        private static Game self;
        
        public Game(Assets assets) {
            self = this;
            this.assets = assets;

            state = GameState.WARMUP;
            
            // Instantiate all systems necessary for other systems to function
            anim = new AnimSystem();
            move = new MovementSystem();
            map = new Map(CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            
            ActionSystem actionSys = new ActionSystem("RL");
            actionSys.OnActionSystemAdded += iActionSystem => {
                if (iActionSystem is IGameSystem gs) {
                    gs.InitGame(this);
                }
            };
            actionSys.RegisterSystems();
            
            Map.CreateRoom(map, new Vector2Int(2, 2), 8, 6);
            MapRenderer.DrawLayer(map, CFG.LAYER_0, assets);

            player = Factory.CreateItem("Player");
            player.SetSprite(Assets.GetEntity(assets, "Player"));
            Map.AddItem(map, player, new Coord(3, 3), CFG.LAYER_1);
            player.AddAction<PickupAction>();
            player.AddAction<PushAction>();
            ItemDataSystem.Set(player, new ItemData {
                strength = 10
            });

            Item sword = Factory.CreateItem("Sword");
            sword.SetSprite(Assets.GetItem(assets, "Sword"));
            Property.Add<PropWeight>(sword);
            Map.AddItem(map, sword, new Coord(new Vector2Int(4, 3)), CFG.LAYER_1);

            Item crate = Factory.CreateItem("Crate");
            crate.SetSprite(Assets.GetItem(assets, "Crate"));
            Map.AddItem(map, crate, new Coord(5, 4), CFG.LAYER_1);
            Property.Add<PropPushable>(crate);

            selection = Factory.CreateItem("Selection");
            selection.SetSprite(Assets.GetMisc(assets, "Selection"));
            Coord selectionCoord = PositionSystem.Get("Selection", new Vector2Int(3, 3));
            selection.SetLocalPosition(selectionCoord.world);
            selection.SetSortingOrder(100);

            Camera.main.transform.position = new Vector3(CFG.MAP_WIDTH  * 0.5f,
                                                         CFG.MAP_HEIGHT * 0.5f,
                                                         -10.0f);
        }

        public GameState Update(float dt) {

            switch (state) {
            case GameState.UNDEFINED:
            case GameState.WARMUP:
                Debug.Log($"GameState.Undefined || GameState.Warmup");
                UpdateSystems();
                state = GameState.AWAITING_PLAYER_INPUT;
            break;
            
            case GameState.AWAITING_PLAYER_INPUT:
                InputState inputState = GetInputState();
                UpdateSelection(inputState.mousePos);
                if (!GetInputCmd(inputState, out inputCmd)) {
                    break;
                }
                Debug.Log($"GameState.AwaitingPlayerInput : Input command {inputCmd}");
                state = GameState.RESOLVE_ACTIONS;
            break;
            
            case GameState.RESOLVE_ACTIONS:
                switch (inputCmd) {
                case InputCommand.WALK:
                    Debug.Log($"GameState.ResolveAction : Move");
                    // move.Move(player, fromCoord, toCoord);
                    anim.DoMove(player.transform, 
                                player.transform.position,
                                selection.transform.position);
                    break;
                case InputCommand.INTERACT:
                    Debug.Log($"GameState.ResolveAction : Interact");
                    // resolve actions
                    break;
                }
                // if left click mouse => walk
                // if left click mouse + shift => sneak
                // if right click mouse => resolve equipped actions with target item
                // get spent energy from energy system
                // make energy available for npcs
                // run npc ai => decide what to do, resolve actions
                // start animations
                // Verbs.Main(this, player, PositionSystem.Get("Selection"));
                anim.Run();
                UpdateSystems();
                state = GameState.PLAY_ANIMATIONS;
            break;
            
            case GameState.PLAY_ANIMATIONS:
                if (anim.IsRunning) {
                    UpdateSystems();
                    break;
                }
                Debug.Log($"GameState.PlayAnimations : done");
                state = GameState.AWAITING_PLAYER_INPUT;
            break;
            }

            if (Property.Has<PropDead>(player)) {
                return GameState.GAME_OVER;
            }

            return state;
        }

        private static InputState GetInputState() {
            InputKey key = InputKey.NONE;
            key |= Input.GetMouseButtonUp(0) ? InputKey.PRIMARY : InputKey.NONE;
            key |= Input.GetMouseButtonUp(1) ? InputKey.SECONDARY : InputKey.NONE;

            Dir dir = Dir.NONE;
            dir |= Input.GetKeyUp(KeyCode.UpArrow) ? Dir.N : Dir.NONE;
            dir |= Input.GetKeyUp(KeyCode.RightArrow) ? Dir.E : Dir.NONE;
            dir |= Input.GetKeyUp(KeyCode.DownArrow) ? Dir.S : Dir.NONE;
            dir |= Input.GetKeyUp(KeyCode.LeftArrow) ? Dir.W : Dir.NONE;

            return new InputState {
                mousePos  = Camera.main.ScreenToWorldPoint(Input.mousePosition),
                key       = key,
                direction = dir,
            };
        }

        private static bool GetInputCmd(InputState inputState, out InputCommand cmd) {
            if ((inputState.key & InputKey.PRIMARY) > 0) {
                cmd = InputCommand.WALK;
                return true;
            }
            
            if ((inputState.key & InputKey.SECONDARY) > 0) {
                cmd = InputCommand.INTERACT;
                return true;
            }

            cmd = InputCommand.UNDEFINED;
            return false;
        }

        // Replace with a mouse based positioning system.
        // Selection is the tile selected by the mouse.
        // The value is found in the input state struct.
        // The position system is quite shit now. Review the code
        // in the rust tutorial to get an idea of how to solve it.
        private void UpdateSelection(Vector3 mousePos) {
            Coord coord = new Coord(mousePos);
            selection.SetLocalPosition(new Vector3(coord.map.x, coord.map.y));
        }

        private void UpdateSystems() {
            // update visibility
            // action system player
            // update monster ai
            // action system monsters
            // play animations
            // map indexing?
        }
    }
}