using System.Collections;
using AptGames;
using Newtonsoft.Json;
using RL.Core;
using RL.Systems.Map;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RL {
    
    public class Bootstrapper : MonoBehaviour, IUpdate {
        
        public Game Game { get; private set; }

        private Context ctx;

        private IEnumerator Start() {
            
            AsyncOperationHandle<TextAsset> configHandle = Addressables.LoadAssetAsync<TextAsset>("config.txt");
            yield return configHandle;
            Config config = JsonConvert.DeserializeObject<Config>(configHandle.Result.text, 
                                                                  new JsonSerializerSettings {
                                                                      TypeNameHandling = TypeNameHandling.Objects
                                                                  });
            if (config == null) {
                Debug.LogError($"Could not deserialize Config with contents: {configHandle.Result.text}");
                yield break;
            }
            
            config.Init();
            
            Assets assets = new Assets();
            yield return assets.LoadSprites("items", "entities", "common");
            yield return assets.LoadPrefabs("system");
            yield return assets.LoadTilemaps(config.Get<MapSystemConfig>().tilemaps);

            ctx = new Context(config, assets, new EventPump());
            Game = new Game(ctx);
            
            UnityUpdate.Add(this);
        }
        
        public void OnUpdate(float dt) {
            Game.Update(dt);
        }
        
        private static string CreateConfigJson() {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting       = Formatting.Indented
            };
        
            Config cfg = new Config(new MapSystemConfig {
                name = "Some kind of name",
                tilemaps = new[] {
                    new TilemapConfig {
                        name         = "brick-wall",
                        group        = Group.Wall,
                        theme        = Theme.Brick,
                        tilingMethod = TilingMethod.AUTOTILE
                    },
                    new TilemapConfig {
                        name         = "stone-floor",
                        group        = Group.Floor,
                        theme        = Theme.Stone,
                        tilingMethod = TilingMethod.RANDOM
                    },
                    new TilemapConfig {
                        name         = "common",
                        group        = Group.Common,
                        tilingMethod = TilingMethod.RANDOM
                    }
                }
            });
            
            return JsonConvert.SerializeObject(cfg, serializerSettings);
        }
    }
}