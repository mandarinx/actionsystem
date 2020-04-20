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
            cfg.tilingMethod = (TilingMethod) Enum.Parse(typeof(TilingMethod),
                                                         cfg.tiling,
                                                         ignoreCase: true);
            cfg.tileIds     = new int[cfg.tileNames.Length];
            cfg.tileSprites = new Dictionary<int, Sprite>();
            cfg.group = (Group) Enum.Parse(typeof(Group),
                                           cfg.groupName ?? "None",
                                           ignoreCase: true);
            cfg.theme = (Theme) Enum.Parse(typeof(Theme),
                                           cfg.themeName ?? "None",
                                           ignoreCase: true);
        }
    }
}