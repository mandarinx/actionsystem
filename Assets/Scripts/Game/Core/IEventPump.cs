
namespace RL.Core {

    public abstract class Event {}

    public delegate void EventDelegate<in T>(T msg) where T : Event;
    public delegate void EventDelegate(Event msg);
    
    public interface IEventPump {

        void Subscribe<T>(EventDelegate<T> del) where T : Event;
        void Unsubscribe<T>(EventDelegate<T> del) where T : Event;
        void Add(Event evt);
        void Dispatch();
    }
}