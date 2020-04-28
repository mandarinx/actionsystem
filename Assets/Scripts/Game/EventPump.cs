using System.Collections.Generic;

namespace RL.Core {

    public class EventPump : IEventPump {

        private readonly Queue<Event>                               eventQueue     = new Queue<Event>();
        private readonly Dictionary<System.Type, EventDelegate>     delegates      = new Dictionary<System.Type, EventDelegate>();
        private readonly Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        public void Subscribe<T>(EventDelegate<T> del) where T : Event {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del)) {
                return;
            }

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            void InternalDelegate(Event msg) => del((T) msg);
            delegateLookup[del] = InternalDelegate;

            if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel)) {
                tempDel += InternalDelegate;
                delegates[typeof(T)] = tempDel;
            } else {
                delegates[typeof(T)] = InternalDelegate;
            }
        }

        public void Unsubscribe<T>(EventDelegate<T> del) where T : Event {
            if (!delegateLookup.TryGetValue(del, out EventDelegate internalDelegate)) {
                return;
            }

            if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel)) {
                tempDel -= internalDelegate;
                if (tempDel == null) {
                    delegates.Remove(typeof(T));
                } else {
                    delegates[typeof(T)] = tempDel;
                }
            }

            delegateLookup.Remove(del);
        }

        public void Add(Event evt) {
            eventQueue.Enqueue(evt);
        }

        public void Dispatch() {
            for (int i = 0; i < eventQueue.Count; ++i) {
                Event evt = eventQueue.Dequeue();
                if (delegates.TryGetValue(evt.GetType(), 
                                          out EventDelegate eventDelegate)) {
                    eventDelegate.Invoke(evt);
                }
            }
        }
    }
}
