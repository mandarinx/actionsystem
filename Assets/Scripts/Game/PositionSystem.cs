using UnityEngine;
using System.Collections.Generic;

namespace RL {

    public static class PositionSystem {
        
        private static readonly Dictionary<string, Coord> coords = new Dictionary<string, Coord>();

        public static Coord Get(string name, Vector2Int defaultValue = default) {
            if (!coords.ContainsKey(name)) {
                coords[name] = new Coord(defaultValue);
            }
            return coords[name];
        }

        public static void Set(string name, Coord coord) {
            coords[name] = coord;
        }

        public static void SetWorldPos(string name, Vector3 worldPos) {
            if (coords.TryGetValue(name, out Coord coord)) {
                Coord.SetWorldPosition(coord, worldPos);
            }
        }
    }
}