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
            UnityUpdate.Add(this);
        }

        public void OnUpdate(float dt) {
            Game.Update();
        }
    }
}