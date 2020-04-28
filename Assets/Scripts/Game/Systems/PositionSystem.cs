using UnityEngine;
using System.Collections.Generic;
using RL.Core;

namespace RL.Systems.Game {

    public class PositionSystem : IGameSystem {
        
        private static readonly Dictionary<int, Vector2Int> positions = new Dictionary<int, Vector2Int>();

        public void Init(IGameSystems gameSystems, Context ctx) {
            // either hook up entity created listener directly to entity system,
            // or hook up with an event pump
        }
    }
}