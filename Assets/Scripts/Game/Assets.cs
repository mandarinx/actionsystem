using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RL {

    public class AssetGroup {

        private readonly string                     assetLabel;
        private readonly Dictionary<string, Sprite> map  = new Dictionary<string, Sprite>();
        private readonly List<Sprite>               list = new List<Sprite>();
        
        public AssetGroup(string label) {
            assetLabel = label;
        }

        public IEnumerator Load() {
            AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetsAsync<Sprite>(assetLabel, null);
            yield return handle;
            for (int n = 0; n < handle.Result.Count; ++n) {
                map.Add(handle.Result[n].name, handle.Result[n]);
                list.Add(handle.Result[n]);
            }
        }

        public static Sprite Get(AssetGroup group, string name) {
            return group.map[name];
        }

        public static Sprite GetRandom(AssetGroup group) {
            return group.list[Random.Range(0, group.list.Count)];
        }
    }
    
    public class Assets {
        private AssetGroup entities = new AssetGroup("entities");
        private AssetGroup items = new AssetGroup("items");
        private AssetGroup floors = new AssetGroup("floors");
        private AssetGroup walls = new AssetGroup("walls");
        private AssetGroup misc = new AssetGroup("misc");

        public IEnumerator Load() {
            yield return entities.Load();
            yield return items.Load();
            yield return walls.Load();
            yield return floors.Load();
            yield return misc.Load();
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

        public static Sprite GetWall(Assets assets, string name) {
            return AssetGroup.Get(assets.walls, name);
        }

        public static Sprite GetRandomWall(Assets assets) {
            return AssetGroup.GetRandom(assets.walls);
        }

        public static Sprite GetFloor(Assets assets, string name) {
            return AssetGroup.Get(assets.floors, name);
        }

        public static Sprite GetRandomFloor(Assets assets) {
            return AssetGroup.GetRandom(assets.floors);
        }

        public static Sprite GetMisc(Assets assets, string name) {
            return AssetGroup.Get(assets.misc, name);
        }

        public static Sprite GetRandomMisc(Assets assets) {
            return AssetGroup.GetRandom(assets.misc);
        }
    }
    
}
