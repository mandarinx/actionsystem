using UnityEngine;

namespace RL {

    public struct Coord {

        public readonly Vector2Int mapCoord;
        public readonly Vector3    worldPos;
        public readonly int        mapIndex;

        public Coord(int mapIndex, Vector2Int mapCoord, Vector3 worldPos) {
            this.mapIndex = mapIndex;
            this.mapCoord = mapCoord;
            this.worldPos = worldPos;
        }

        public override string ToString() {
            return $"Coord {{mapCoord: {mapCoord}, worldPos: {worldPos}, mapIndex: {mapIndex}}}";
        }

        public static bool Equals(Coord a, Coord b) {
            return a.mapIndex == b.mapIndex;
        }
    }
}