namespace RL.Core {

    public interface IGameSystems {

        void Add(IGameSystem iGameSystem);
        void Init(IGameSystems gameSystems, IConfig config, IAssets assets);
    }
}