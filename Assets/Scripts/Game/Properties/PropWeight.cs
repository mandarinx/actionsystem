using Altruist;
using UnityEngine;

namespace RL {

    [AddComponentMenu("Properties/Weight")]
    public class PropWeight : MonoBehaviour, IProperty {
        [SerializeField] private int weight = 0;
        public int Weight => weight;
    }
}
