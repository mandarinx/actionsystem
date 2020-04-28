
namespace RL.Core {

    public class Context {
        public IEventPump eventPump;
        public IAssets assets;
        public Config config;

        public Context(Config Config, IAssets iAssets, IEventPump iEventPump) {
            eventPump = iEventPump;
            config = Config;
            assets = iAssets;
        }
    }
}