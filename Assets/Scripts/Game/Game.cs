using System;
using RL.Core;
using RL.Systems.Game;
using RL.Systems.Items;
using RL.Systems.Map;
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

        public readonly GameSystems systems;

        // consider moving player and selection to separate systems
        private readonly Item      player;
        private readonly Item      selection;
        
        private GameState state;
        private InputCommand inputCmd;
        private Context ctx;
        
        public Game(Context ctx) {
            this.ctx = ctx;
            
            state = GameState.UNDEFINED;
            
            systems = new GameSystems();
            systems.Add(new AnimSystem());
            systems.Add(new MovementSystem());
            systems.Add(new MapSystem(ctx.config.Get<MapSystemConfig>()));
            systems.Add(new CameraSystem());
            systems.Add(new EntitySystem());
            
            systems.Init(systems, ctx);
            
            ActionSystem actionSys = new ActionSystem(assemblyNames: "Game");
            actionSys.OnActionSystemRegistered += iActionSystem => {
                if (iActionSystem is IGameSystem gs) {
                    gs.Init(systems, ctx);
                }
            };
            actionSys.RegisterSystems();

            MapSystem mapSystem = systems.Get<MapSystem>();
            
            mapSystem.Load(new[] {
                0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,
                0x1,0xB,0xB,0xB,0xB,0x1000A,0xB,0xB,0xB,0x1,0x1,0x1,
                0x1,0xB,0xA,0xA,0xA,0xA,0xA,0xA,0xB,0x1,0x1,0x1,
                0x1,0xB,0xA,0xA,0xA,0xA,0xA,0xA,0xB,0x1,0x1,0x1,
                0x1,0xB,0xA,0xA,0xA,0xA,0xA,0xA,0xB,0x1,0x1,0x1,
                0x1,0xB,0xB,0xB,0xB,0xB,0xB,0xB,0xB,0x1,0x1,0x1,
                0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,
                0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,0x1,
            }, 12, "Entrance");
            
            player = systems.Get<EntitySystem>().CreatePlayer("Player");
            int spawnpoint = mapSystem.Map.Spawnpoint;
            player.SetLocalPosition(mapSystem.Map.IndexToWorldPos(spawnpoint));

            ctx.eventPump.Dispatch();
            // systems.Get<CameraSystem>().SetTarget(player.transform);
        }

        public void Update(float dt) {
            switch (state) {
            case GameState.UNDEFINED:
                state = GameState.AWAITING_PLAYER_INPUT;
            break;
            
            case GameState.AWAITING_PLAYER_INPUT:
                InputState inputState = GetInputState();
                UpdateSelection(inputState.mousePos);
                if (!GetInputCmd(inputState, out inputCmd)) {
                    break;
                }
                state = GameState.RESOLVE_PLAYER_ACTIONS;
            break;
            
            case GameState.RESOLVE_PLAYER_ACTIONS:
                switch (inputCmd) {
                case InputCommand.WALK:
                    // systems.Get<MovementSystem>().Move(player,
                    //                                    );
                    break;
                case InputCommand.INTERACT:
                    // resolve actions -> new animations
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
                
                inputCmd = InputCommand.UNDEFINED; // consume the command
                state = GameState.RESOLVE_NPC_ACTIONS;
            break;
            
            case GameState.RESOLVE_NPC_ACTIONS:
                // update npc ai
                // resolve actions -> new animations
                state = GameState.WILL_UPDATE_SYSTEMS;
            break;
            
            case GameState.WILL_UPDATE_SYSTEMS:
                ctx.eventPump.Dispatch();
                systems.Get<AnimSystem>().Run();
                state = GameState.UPDATE_SYSTEMS;
            break;
            
            case GameState.UPDATE_SYSTEMS:
                if (systems.Get<AnimSystem>().IsRunning) {
                    // update sight system
                    // update audio system
                    // update position system
                    break;
                }
                state = GameState.AWAITING_PLAYER_INPUT;
            break;
            }
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
            // Coord coord = new Coord(mousePos);
            // selection.SetLocalPosition(new Vector3(coord.map.x, coord.map.y));
        }
    }
}