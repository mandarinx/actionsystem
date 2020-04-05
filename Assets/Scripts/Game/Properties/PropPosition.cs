using UnityEngine;

namespace RL {

    [AddComponentMenu("Properties/Position")]
    public class PropPosition : MonoBehaviour, IProperty {

        public static void SetCoord(PropPosition prop, Coord coord) {
            PositionSystem.Set(prop.gameObject.name, coord);
        }

        public static void SetWorldPos(PropPosition prop, Vector3 worldPos) {
            PositionSystem.SetWorldPos(prop.gameObject.name, worldPos);
        }

        public static Coord GetCoord(PropPosition prop) {
            return PositionSystem.Get(prop.gameObject.name);
        }
    }
}
