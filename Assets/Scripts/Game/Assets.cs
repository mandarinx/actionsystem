using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RL.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RL.Systems.Map;
using Object = UnityEngine.Object;

namespace RL {
    
    public class Assets : IAssets {
        private readonly Dictionary<string, Sprite>        tableSprites  = new Dictionary<string, Sprite>();
        private readonly Dictionary<string, GameObject>    tablePrefabs  = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, TilemapAssets> tableTilemaps = new Dictionary<string, TilemapAssets>();

        public IEnumerator LoadSprites(params string[] assetLabels) {
            List<AsyncOperationHandle<IList<Sprite>>> handles = new List<AsyncOperationHandle<IList<Sprite>>>();
            for (int i = 0; i < assetLabels.Length; ++i) {
                assetLabels[i] = assetLabels[i].ToLower();
            }
            for (int i = 0; i < assetLabels.Length; ++i) {
                handles.Add(Addressables.LoadAssetsAsync<Sprite>($"sprites-{assetLabels[i]}", null));
            }
            for (int i = 0; i < handles.Count; ++i) {
                yield return handles[i];
            }
            for (int i = 0; i < handles.Count; ++i) {
                CacheResults(assetLabels[i], handles[i].Result, tableSprites);
            }
        }

        public IEnumerator LoadPrefabs(params string[] assetLabels) {
            List<AsyncOperationHandle<IList<GameObject>>> handles = new List<AsyncOperationHandle<IList<GameObject>>>();
            for (int i = 0; i < assetLabels.Length; ++i) {
                assetLabels[i] = assetLabels[i].ToLower();
            }
            for (int i = 0; i < assetLabels.Length; ++i) {
                handles.Add(Addressables.LoadAssetsAsync<GameObject>($"prefabs-{assetLabels[i]}", null));
            }
            for (int i = 0; i < handles.Count; ++i) {
                yield return handles[i];
            }
            for (int i = 0; i < handles.Count; ++i) {
                CacheResults(assetLabels[i], handles[i].Result, tablePrefabs);
            }
        }
        
        public IEnumerator LoadTilemaps(TilemapConfig[] tilemapConfigs) {
            for (int i = 0; i < tilemapConfigs.Length; ++i) {
                yield return LoadTilemap(tilemapConfigs[i], tableTilemaps);
            }
        }

        private static IEnumerator LoadTilemap(TilemapConfig                      config,
                                               IDictionary<string, TilemapAssets> table) {
            List<AsyncOperationHandle<Sprite>> handles = new List<AsyncOperationHandle<Sprite>>();
            
            if (config.group == Group.None) {
                Debug.LogError("Cannot load TilemapConfig without group name");
                yield break;
            }

            string baseAddress = $"Tiles/{Utils.Capitalize(config.group.ToString())}";
            if (config.theme != Theme.None) {
                baseAddress = $"{baseAddress}/{Utils.Capitalize(config.theme.ToString())}";
            }
            
            TilemapAssets assets = new TilemapAssets();

            switch (config.tilingMethod) {
                case TilingMethod.AUTOTILE: {
                    assets.tileIds = new int[16];
                    
                    for (int i = 0; i < 16; ++i) {
                        string address = $"{baseAddress}/{CFG.TILE_TABLE_WALLS[i]}.png";
                        handles.Add(Addressables.LoadAssetAsync<Sprite>(address));
                    }
                    break;
                }
                
                case TilingMethod.RANDOM:
                case TilingMethod.PERLINNOISE:
                    assets.tileIds = new int[config.tileNames.Length];
                    
                    for (int i = 0; i < config.tileNames.Length; ++i) {
                        string address = $"{baseAddress}/{config.tileNames[i]}.png";
                        handles.Add(Addressables.LoadAssetAsync<Sprite>(address));
                    }
                    break;
            }

            for (int i = 0; i < handles.Count; ++i) {
                yield return handles[i];
            }

            for (int i = 0; i < handles.Count; ++i) {
                // Debug.Log($"Loaded {baseAddress}/{handles[i].Result.name}");
                assets.tileSprites.Add(i, handles[i].Result);
                assets.tileIds[i] = i;
            }
            
            table.Add(config.name, assets);
        }

        private static void CacheResults<T>(string                 label,
                                            IList<T>               result,
                                            IDictionary<string, T> table) where T : Object {
            for (int n = 0; n < result.Count; ++n) {
                Debug.Log($"{label}/{result[n].name.ToLower()}");
                table.Add($"{label}/{result[n].name.ToLower()}", result[n]);
            }
        }

        public T Get<T>(string group, string name) where T : Object {
            switch (group) {
                case "sprites":
                    return tableSprites.TryGetValue(name, out Sprite sprite) ? sprite as T : default;
                case "prefabs":
                    return tablePrefabs.TryGetValue(name, out GameObject prefab) ? prefab as T : default;
                case "tilemaps":
                    return tableTilemaps.TryGetValue(name, out TilemapAssets tilemapAssets) ? tilemapAssets as T : default;
                default:
                    return default;
            }
        }

        public Sprite GetSprite(string name) {
            return Get<Sprite>("sprites", name);
        }
    }
    
}
