using Altruist;
using UnityEngine;

namespace RL {

    public class Game {
        
        public readonly Map map;

        private readonly Item        player;
        private readonly Item        selection;
        private readonly MapRenderer mapRen;
        private readonly Assets      assets;

        public Game(Assets assets) {
            this.assets = assets;
            
            ActionSystem actionSys = new ActionSystem("RL");
            actionSys.RegisterSystems();
            
            map = new Map(CFG.MAP_WIDTH, CFG.MAP_HEIGHT);
            mapRen = new MapRenderer(CFG.MAP_WIDTH, CFG.MAP_HEIGHT);

            Map.CreateRoom(map, new Vector2Int(2, 2), 8, 6);

            player = Factory.CreateItem("Player");
            Item.SetSprite(player, Assets.GetEntity(assets, "Player"));
            Coord playerCoord = PositionSystem.Get("Player", new Vector2Int(3, 3));
            Item.SetLocalPosition(player, playerCoord.world);
            Item.SetSortingOrder(player, 10);
            Action.Add<PickupAction>(player);
            Action.Add<MoveAction>(player);
            ItemDataSystem.Set(player, new ItemData {
                strength = 10
            });

            selection = Factory.CreateItem("Selection");
            Item.SetSprite(selection, Assets.GetMisc(assets, "Selection"));
            Coord selectionCoord = PositionSystem.Get("Selection", new Vector2Int(3, 3));
            Item.SetLocalPosition(selection, selectionCoord.world);
            Item.SetSortingOrder(selection, 100);

            Item sword = Factory.CreateItem("Sword");
            Item.SetSprite(sword, Assets.GetItem(assets, "Sword"));
            Item.SetSortingOrder(sword, 1);
            Property.Add<PropWeight>(sword);
            Coord swordCoord = new Coord(new Vector2Int(4, 3));
            Inventory.Add(MapRenderer.GetTile(mapRen, swordCoord).Items, sword);

            Item gameOver = Factory.CreateItem("GameOver");
            Property.Add<PropVoid>(gameOver);
            Action.Add<GameOverAction>(gameOver);
            Property.RegisterAddEvent<PropDead>(player, gameOver);
            
            Camera.main.transform.position = new Vector3(CFG.MAP_WIDTH  * 0.5f,
                                                         CFG.MAP_HEIGHT * 0.5f,
                                                         -10.0f);
        }

        public static void Draw(Game game) {
            MapRenderer.DrawLayer(game.mapRen, game.map, game.assets, 0);
        }

        public static void Update(Game game) {
            Coord sc = PositionSystem.Get("Selection");
            Coord pc = PositionSystem.Get("Player");
            // use a state machine for handling player and game turn
            
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
            Item.SetLocalPosition(game.selection, sc.world);

            if (Input.GetKeyUp(KeyCode.Space)) {
                Verbs.Main(game.player, 
                           MapRenderer.GetTile(game.mapRen, sc.index));
            }
            
            // Create a separate runner, just for player actions
            // while (Coroutines.IsRunning) {
            //   return;
            // }
            
            // Advance game
        }
    }
}