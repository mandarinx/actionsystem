using System.Collections.Generic;
using UnityEngine;

namespace RL {
    public static class CFG {
        // public const int MAP_WIDTH  = 16;
        // public const int MAP_HEIGHT = 10;

        public const int LAYER_0 = 0;
        public const int LAYER_1 = 1;
        
        public const string PPKEY_DRAW_GRID = "dbgDrawGrid";

        public const float  E_SPEED = 10f;
        public const int    EID_PLAYER      = 1000;
        public const string E_PLAYER_NAME = "Player";

        // tile types
        // reserve some bits for tile group, some for theme/variation, maybe some for meta data
        // Group
        // 9 bits for group = 512 groups.
        // A group is like wall, floor, ground, etc
        // Theme/variation
        // 9 bits for theme = 512 themes.
        // Theme is used for variation of group. Wooden (wall), stone (floor), metal (wall/floor),
        // dry (grass), wet (grass, floor), icy (water, grass), lava (stone), etc
        // Meta data
        // 14 bits for meta data = 16.384 bits
        // Reserved for what ever extra data. Whether a tile is walkable, what traverse cost it has, etc
        
        public const int TT_VOID  = 0;
        public const int TT_FLOOR = 1;
        public const int TT_WALL  = 2;
        
        public static readonly Dictionary<int, string> WALL_MAP = new Dictionary<int, string> {
            { 0,  "Orphan" },
            { 1,  "W" },
            { 2,  "S" },
            { 3,  "SW" },
            { 4,  "E" },
            { 5,  "H" },
            { 6,  "ES" },
            { 7,  "TS" },
            { 8,  "N" },
            { 9,  "NW" },
            { 10, "V" },
            { 11, "TW" },
            { 12, "NE" },
            { 13, "TN" },
            { 14, "TE" },
            { 15, "X" },
        };
        public const int T_BLANK       = 0;
        public const int T_FLOOR_SLABS = 0;
        public const int T_WALL_N      = 8;
        public const int T_WALL_E      = 4;
        public const int T_WALL_S      = 2;
        public const int T_WALL_W      = 1;
        public const int T_WALL_NE     = 12;
        public const int T_WALL_ES     = 6;
        public const int T_WALL_SW     = 3;
        public const int T_WALL_WN     = 9;
        public const int T_WALL_H      = 5;
        public const int T_WALL_V      = 10;
        public const int T_WALL_TN     = 13;
        public const int T_WALL_TE     = 14;
        public const int T_WALL_TS     = 7;
        public const int T_WALL_TW     = 11;
        public const int T_WALL_X      = 15;
        public const int T_WALL_ORPHAN = 0;

        public const int FLAG_N = 1 << 3;
        public const int FLAG_E = 1 << 2;
        public const int FLAG_S = 1 << 1;
        public const int FLAG_W = 1 << 0;

        // public static readonly Dictionary<int, Sprite> SPRITES_WALL     = new Dictionary<int, Sprite>();
        // public static readonly Dictionary<int, Sprite> SPRITES_FLOOR    = new Dictionary<int, Sprite>();
        // public static readonly Dictionary<int, Sprite> SPRITES_VOID     = new Dictionary<int, Sprite>();
        // // indexed by entity id
        // public static readonly Dictionary<int, Sprite> SPRITES_ENTITIES = new Dictionary<int, Sprite>();
        
        public static readonly Dictionary<int, string> ENTITY_NAMES = new Dictionary<int, string> {
            { EID_PLAYER, E_PLAYER_NAME }
        };

        // public static readonly string[] ENTITIES = {
        //     $"Entity/{ENTITY_NAMES[E_PLAYER]}",
        // };

        public static readonly int[] ENTITY_IDS = {
            EID_PLAYER,
        };

        // public static readonly string[] TILES_VOID = {
        //     $"Tile/{T_BLANK}",
        // };
        //
        // public static readonly string[] TILES_FLOOR = {
        //     $"Floor/{T_FLOOR_SLABS}",
        // };
            
        // public static readonly string[] TILES_WALL = {
        //     $"Wall/{T_WALL_ORPHAN}",
        //     $"Wall/{T_WALL_W}",
        //     $"Wall/{T_WALL_S}",
        //     $"Wall/{T_WALL_SW}",
        //     $"Wall/{T_WALL_E}",
        //     $"Wall/{T_WALL_H}",
        //     $"Wall/{T_WALL_ES}",
        //     $"Wall/{T_WALL_TS}",
        //     $"Wall/{T_WALL_N}",
        //     $"Wall/{T_WALL_WN}",
        //     $"Wall/{T_WALL_V}",
        //     $"Wall/{T_WALL_TW}",
        //     $"Wall/{T_WALL_NE}",
        //     $"Wall/{T_WALL_TN}",
        //     $"Wall/{T_WALL_TE}",
        //     $"Wall/{T_WALL_X}",
        // };
    }
}
