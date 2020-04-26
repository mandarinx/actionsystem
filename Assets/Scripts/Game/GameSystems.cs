using System.Collections.Generic;
using RL.Core;

namespace RL.Systems.Game {
    public class GameSystems : IGameSystems {
        
        internal readonly List<IGameSystem> systems = new List<IGameSystem>();

        public void Add(IGameSystem iGameSystem) {
            systems.Add(iGameSystem);
        }

        public void Init(IGameSystems gameSystems, IConfig config, IAssets assets) {
            for (int i = 0; i < systems.Count; ++i) {
                systems[i].Init(gameSystems, config, assets);
            }
        }

        public T Get<T>() where T : IGameSystem {
            for (int i = 0; i < systems.Count; ++i) {
                if (systems[i] is T) {
                    return (T) systems[i];
                }
            }
            return default;
        }
    }
}