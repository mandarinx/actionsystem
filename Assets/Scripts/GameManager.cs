using System.Collections;
using AptGames;
using UnityEngine;

namespace RL {
    
    public class GameManager : MonoBehaviour, IUpdate {
        
        public Game Game { get; private set; }

        private readonly Assets assets = new Assets();

        private IEnumerator Start() {
            yield return assets.Load();
            Game = new Game(assets);
            Game.Draw(Game);
            UnityUpdate.Add(this);
        }

        public void OnUpdate(float dt) {
            Game.Update(Game);
        }
    }

    // public class EntityConfig {
    //     public string name;
    //     public IAction[] actions;
    // }
}