namespace RL.Core {
    public interface IAssets {
        T Get<T>(string group, string name) where T : UnityEngine.Object;
    }
}