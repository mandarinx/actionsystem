using System.Collections;
using AptGames;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RL {
    
    // inspired by ggez rust crate
    // https://github.com/ggez/ggez
    
    // consider creating a context object, that holds common utilities lik
    // - audio playing lib
    // - event loop?
    //   > dispatches system wide events. expects objects to be added via their interfaces, like
    //     IGameEventAwake, IGameEventShutdown, IGameEventUpdate, etc
    //   > could be passed inbto the context. that way the game loop can easily be customized
    // - input system
    // - file system lib
    // - message system
    // - config
    //   > system config, like window size, fullscreen, audio level
    
    
    public class Bootstrapper : MonoBehaviour, IUpdate {
        
        public Game Game { get; private set; }

        private IEnumerator Start() {
            AsyncOperationHandle<TextAsset> configHandle = Addressables.LoadAssetAsync<TextAsset>("config.txt");
            yield return configHandle;
            Config config = JsonUtility.FromJson<Config>(configHandle.Result.text);
            Config.Parse(config);

            Assets assets = new Assets();
            yield return assets.Load(config.mapSystem.tilemaps);
            
            Game = new Game(config, assets);
            UnityUpdate.Add(this);
        }
        public void OnUpdate(float dt) {
            Game.Update(dt);
        }
    }
}