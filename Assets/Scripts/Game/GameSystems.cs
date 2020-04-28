using System;
using System.Collections.Generic;
using RL.Core;

namespace RL.Systems.Game {
    public class GameSystems : IGameSystems {

        internal readonly List<IGameSystem>             systems      = new List<IGameSystem>();
        internal readonly Dictionary<Type, IGameSystem> systemsTable = new Dictionary<Type, IGameSystem>();

        public void Add(IGameSystem iGameSystem) {
            Type gameSystemType = iGameSystem.GetType();
            if (systemsTable.ContainsKey(gameSystemType)) {
                return;
            }
            systems.Add(iGameSystem);
            systemsTable.Add(gameSystemType, iGameSystem);
        }

        public void Init(IGameSystems gameSystems, Context context) {
            for (int i = 0; i < systems.Count; ++i) {
                systems[i].Init(gameSystems, context);
            }
        }

        public T Get<T>() where T : IGameSystem {
            return systemsTable.TryGetValue(typeof(T), out IGameSystem gameSystem) 
                ? (T) gameSystem 
                : default;
        }
    }
}