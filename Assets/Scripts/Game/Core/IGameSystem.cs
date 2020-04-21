namespace RL.Core {

    public interface IGameSystem {
        void Init(IGameSystems gameSystems, IConfig config, IAssets assets);
    }
}