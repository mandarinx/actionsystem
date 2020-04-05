using UnityEngine;
using System.Collections.Generic;

namespace RL {

    // Responsibility:
    // - Tell if an Item is at a coordinate
    // ~ Lookup a coordinate by name
    
    public static class PositionSystem {
        
        private static readonly Dictionary<string, Coord> coords = new Dictionary<string, Coord>();

        public static Coord Get(string name, Vector2Int defaultValue = default) {
            if (!coords.ContainsKey(name)) {
                coords[name] = new Coord(defaultValue);
            }
            return coords[name];
        }

        public static void Set(Item item, Coord coord) {
            coords[item.name] = coord;
            if (!Property.Has<PropPosition>(item)) {
                Property.Add<PropPosition>(item);
            }
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