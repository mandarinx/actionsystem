using System;
using System.Collections.Generic;
using UnityEngine;

namespace RL {

    [Serializable]
    public class TilemapConfig {

        [SerializeField] private string       tiling;
        [NonSerialized]  public  TilingMethod tilingMethod;
        
        public string   group;        // Hash to int
        public string   theme;        // Hash to int
        public string   walkable;     // Parse as int

        [SerializeField] public string[]                tilenames = new string[0];
        [NonSerialized]  public Dictionary<int, Sprite> tileSprites;
        [NonSerialized]  public int[]                   tileIds;

        public static void Parse(TilemapConfig cfg) {
            cfg.tilingMethod = (TilingMethod) Enum.Parse(typeof(TilingMethod),
                                                         cfg.tiling.ToUpper());
            cfg.tileIds     = new int[cfg.tilenames.Length];
            cfg.tileSprites = new Dictionary<int, Sprite>();
        }
    }
}