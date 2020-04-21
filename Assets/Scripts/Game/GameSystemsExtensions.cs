using RL.Systems.Game;
using RL.Systems.Map;

namespace RL {

    public static class GameSystemsExtensions {

        public static MapSystem GetMapSystem(this GameSystems gameSystems) {
            return gameSystems.systems[2] as MapSystem;
        }

        public static Factory GetFactory(this GameSystems gameSystems) {
            return gameSystems.systems[3] as Factory;
        }
    }
}