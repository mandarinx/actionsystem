using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;
using RL.Systems.Map;

namespace RL {

    public class AssetGroup {

        private readonly string                     assetLabel;
        private readonly Dictionary<string, Sprite> table = new Dictionary<string, Sprite>();
        private readonly List<Sprite>               list = new List<Sprite>();
        
        public AssetGroup(string label) {
            assetLabel = label;
        }

        public IEnumerator Load() {
            AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetsAsync<Sprite>(assetLabel, null);
            yield return handle;
            for (int n = 0; n < handle.Result.Count; ++n) {
                table.Add(handle.Result[n].name, handle.Result[n]);
                list.Add(handle.Result[n]);
            }
        }

        public static Sprite Get(AssetGroup group, string name) {
            return group.table[name];
        }

        public static Sprite GetRandom(AssetGroup group) {
            return group.list[Random.Range(0, group.list.Count)];
        }
    }
    
    public class Assets {
        private AssetGroup entities = new AssetGroup("entities");
        private AssetGroup items = new AssetGroup("items");
        private AssetGroup misc = new AssetGroup("misc");

        public IEnumerator Load(TilemapConfig[] tilemapConfigs) {
            for (int i = 0; i < tilemapConfigs.Length; ++i) {
                yield return LoadTilemap(tilemapConfigs[i]);
            }
            yield return entities.Load();
            yield return items.Load();
            yield return misc.Load();
        }

        private IEnumerator LoadTilemap(TilemapConfig config) {
            List<AsyncOperationHandle<Sprite>> handles = new List<AsyncOperationHandle<Sprite>>();
            if (config.group == Group.None) {
                Debug.LogError("Cannot load TilemapConfig without group name");
                yield break;
            }

            string baseAddress = $"{Utils.Capitalize(config.groupName)}";
            if (config.theme != Theme.None) {
                baseAddress = $"{baseAddress}/{Utils.Capitalize(config.themeName)}";
            }

            switch (config.tilingMethod) {
                case TilingMethod.AUTOTILE: {
                    config.tileIds = new int[16];
                    
                    for (int i = 0; i < 16; ++i) {
                        string address = $"{baseAddress}/{CFG.WALL_MAP[i]}.png";
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

        public static Sprite GetEntity(Assets assets, string name) {
            return AssetGroup.Get(assets.entities, name);
        }

        public static Sprite GetRandomEntity(Assets assets) {
            return AssetGroup.GetRandom(assets.entities);
        }

        public static Sprite GetItem(Assets assets, string name) {
            return AssetGroup.Get(assets.items, name);
        }

        public static Sprite GetRandomItem(Assets assets) {
            return AssetGroup.GetRandom(assets.items);
        }

        public static Sprite GetMisc(Assets assets, string name) {
            return AssetGroup.Get(assets.misc, name);
        }

        public static Sprite GetRandomMisc(Assets assets) {
            return AssetGroup.GetRandom(assets.misc);
        }
    }
    
}
