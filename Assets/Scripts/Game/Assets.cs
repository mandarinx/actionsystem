using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RL.Core;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;
using RL.Systems.Map;

namespace RL {

    public class AssetGroup<T> where T : Object {

        private readonly string                     assetLabel;
        private readonly Dictionary<string, T> table = new Dictionary<string, T>();
        private readonly List<T>               list = new List<T>();
        
        public AssetGroup(string label) {
            assetLabel = label;
        }

        public IEnumerator Load() {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(assetLabel, null);
            yield return handle;
            for (int n = 0; n < handle.Result.Count; ++n) {
                table.Add(handle.Result[n].name, handle.Result[n]);
                list.Add(handle.Result[n]);
            }
        }

        public T Get(string name) {
            return table[name];
        }

        public T GetRandom() {
            return list[Random.Range(0, list.Count)];
        }
    }
    
    public class Assets : IAssets {
        private readonly AssetGroup<Sprite> entities = new AssetGroup<Sprite>("entities");
        private readonly AssetGroup<Sprite> items = new AssetGroup<Sprite>("items");
        private readonly AssetGroup<Sprite> misc = new AssetGroup<Sprite>("misc");
        private readonly AssetGroup<GameObject> prefabs = new AssetGroup<GameObject>("prefabs");

        public IEnumerator Load(TilemapConfig[] tilemapConfigs) {
            for (int i = 0; i < tilemapConfigs.Length; ++i) {
                yield return LoadTilemap(tilemapConfigs[i]);
            }
            yield return entities.Load();
            yield return items.Load();
            yield return misc.Load();
            yield return prefabs.Load();
        }

        private IEnumerator LoadTilemap(TilemapConfig config) {
            List<AsyncOperationHandle<Sprite>> handles = new List<AsyncOperationHandle<Sprite>>();
            if (config.group == Group.None) {
                Debug.LogError("Cannot load TilemapConfig without group name");
                yield break;
            }

            string baseAddress = $"Tiles/{Utils.Capitalize(config.groupName)}";
            if (config.theme != Theme.None) {
                baseAddress = $"{baseAddress}/{Utils.Capitalize(config.themeName)}";
            }

            switch (config.tilingMethod) {
                case TilingMethod.AUTOTILE: {
                    config.tileIds = new int[16];
                    
                    for (int i = 0; i < 16; ++i) {
                        string address = $"{baseAddress}/{CFG.TILE_TABLE_WALLS[i]}.png";
                        handles.Add(Addressables.LoadAssetAsync<Sprite>(address));
                    }
                    break;
                }
                
                case TilingMethod.RANDOM:
                case TilingMethod.PERLINNOISE:
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
                config.tileSprites.Add(i, handles[i].Result);
                config.tileIds[i] = i;
            }
        }

        public Sprite GetEntity(string name) {
            return entities.Get(name);
        }

        public Sprite GetRandomEntity() {
            return entities.GetRandom();
        }

        public Sprite GetItem(string name) {
            return items.Get(name);
        }

        public Sprite GetRandomItem() {
            return items.GetRandom();
        }

        public Sprite GetMisc(string name) {
            return misc.Get(name);
        }

        public Sprite GetRandomMisc() {
            return misc.GetRandom();
        }

        public GameObject GetPrefab(string name) {
            return prefabs.Get(name);
        }
    }
    
}
