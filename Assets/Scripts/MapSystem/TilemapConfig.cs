using System;
using System.Collections.Generic;
using UnityEngine;

namespace RL.Systems.Map {

    [Serializable]
    public class TilemapConfig {

        [SerializeField] private string       tiling;
        [NonSerialized]  public  TilingMethod tilingMethod;

        [SerializeField] public string groupName;
        [NonSerialized]  public Group  group;
        [SerializeField] public string themeName;
        [NonSerialized]  public Theme  theme;

        [SerializeField] public string[]                tileNames = new string[0];
        [NonSerialized]  public Dictionary<int, Sprite> tileSprites;
        [NonSerialized]  public int[]                   tileIds;

        public static void Parse(TilemapConfig cfg) {
            cfg.tilingMethod = Parse<TilingMethod>(cfg.tiling, "undefined");
            cfg.tileIds      = new int[cfg.tileNames.Length];
            cfg.tileSprites  = new Dictionary<int, Sprite>();
            cfg.group        = Parse<Group>(cfg.groupName, "None");
            cfg.theme        = Parse<Theme>(cfg.themeName, "None");
        }

        private static T Parse<T>(string value, string defaults) {
            return (T)Enum.Parse(typeof(T), value ?? defaults, ignoreCase: true);
        }
    }
}