using System.Collections.Generic;
using UnityEngine;

namespace RL.Systems.Map {

    public class TilemapConfig {
        public string       name;
        public TilingMethod tilingMethod;
        public Group        group;
        public Theme        theme;
        public string[]     tileNames = new string[0];
    }

    public class TilemapAssets {
        public readonly Dictionary<int, Sprite> tileSprites;
        public          int[]                   tileIds;

        public TilemapAssets() {
            tileSprites = new Dictionary<int, Sprite>();
        }
    }
}