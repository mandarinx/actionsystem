using System.Collections;
using AptGames;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RL {
    
    public class Bootstrapper : MonoBehaviour, IUpdate {
        
        public Game Game { get; private set; }

        private IEnumerator Start() {
            // why is config a text file?
            AsyncOperationHandle<TextAsset> configHandle = Addressables.LoadAssetAsync<TextAsset>("config.txt");
            yield return configHandle;
            Config config = JsonUtility.FromJson<Config>(configHandle.Result.text);
            Config.Parse(config);

            Assets assets = new Assets();
            yield return assets.Load(config.tilemaps);
            
            Game = new Game(config, assets);
            UnityUpdate.Add(this);
        }
        public void OnUpdate(float dt) {
            Game.Update(dt);
        }
    }
}