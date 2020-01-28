using UnityEngine;
using Altruist;

namespace RL {

    [AddComponentMenu("Properties/Position")]
    public class PropPosition : MonoBehaviour, IProperty {
        [SerializeField] private string posName;

        public static void SetCoord(PropPosition prop, Coord coord) {
            PositionSystem.Set(prop.posName, coord);
        }

        public static void SetWorldPos(PropPosition prop, Vector3 worldPos) {
            PositionSystem.SetWorldPos(prop.posName, worldPos);
        }

        public static Coord GetCoord(PropPosition prop) {
            return PositionSystem.Get(prop.posName);
        }

        public static void SetName(PropPosition prop, string name) {
            prop.posName = name;
        }

        public static string GetName(PropPosition prop) {
            return prop.posName;
        }
    }
}
