using Altruist;
using UnityEngine;

namespace RL {

    public class Game {
        
        public readonly Map map;

        private readonly Item         player;
        private readonly Item         selection;
        private readonly Assets       assets;
        private readonly ActionRunner actionRunnerPlayer;

        public Game(Assets assets) {
            this.assets = assets;
            
            ActionSystem actionSys = new ActionSystem("RL");
            actionSys.RegisterSystems();

            actionRunnerPlayer = ActionRunner.Create("ActionRunner_Player");
            
            map = new Map(CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            Map.CreateRoom(map, new Vector2Int(2, 2), 8, 6);
            MapRenderer.DrawLayer(map, CFG.LAYER_0, assets);

            player = Factory.CreateItem("Player");
            Item.SetSprite(player, Assets.GetEntity(assets, "Player"));
            Map.AddItem(map, player, new Coord(3, 3), CFG.LAYER_1);
            Action.Add<PickupAction>(player);
            Action.Add<MoveAction>(player);
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
            Action.Add<MoveAction>(crate);

            selection = Factory.CreateItem("Selection");
            Item.SetSprite(selection, Assets.GetMisc(assets, "Selection"));
            Coord selectionCoord = PositionSystem.Get("Selection", new Vector2Int(3, 3));
            Item.SetLocalPosition(selection, selectionCoord.world);
            Item.SetSortingOrder(selection, 100);

            actionSys.BridgeWith(map);
            actionSys.BridgeWith(actionRunnerPlayer);
            
            Camera.main.transform.position = new Vector3(CFG.MAP_WIDTH  * 0.5f,
                                                         CFG.MAP_HEIGHT * 0.5f,
                                                         -10.0f);
        }

        public void Update() {

            while (actionRunnerPlayer.IsRunning) {
                return;
            }
            
            // while npc actions running
            // > return

            if (Property.Has<PropDead>(player)) {
                Debug.Log("GAME OVER");
                return;
            }

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

            Verbs.Main(this, player, sc, actionRunnerPlayer);
        }
    }
}