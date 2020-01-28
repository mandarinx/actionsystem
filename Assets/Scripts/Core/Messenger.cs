using System;
using System.Collections.Generic;

namespace Altruist {

    public abstract class Msg {}

    public delegate bool MessagePredicate (Msg msg);
    public delegate void MessageDelegate (Msg msg);
    public delegate void MessageDelegate<in T> (T msg) where T : Msg;

    public class Messenger {

        private readonly Dictionary<Type, MessageDelegate>     delegates      = new Dictionary<Type, MessageDelegate>();
        private readonly Dictionary<Delegate, MessageDelegate> delegateLookup = new Dictionary<Delegate, MessageDelegate>();
        private readonly Dictionary<Type, MessagePredicate>    predicates     = new Dictionary<Type, MessagePredicate>();

        public Messenger AddListener<T>(MessageDelegate<T> del, Predicate<T> validator = null) where T : Msg {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del)) {
                return this;
            }

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            void InternalDelegate(Msg msg) => del((T) msg);
            delegateLookup[del] = InternalDelegate;

            if (validator != null) {
                bool Pdel(Msg msg) => validator((T) msg);
                predicates.Add(typeof(T), Pdel);
            }

            if (delegates.ContainsKey(typeof(T))) {
                delegates[typeof(T)] += InternalDelegate;
            } else {
                delegates[typeof(T)] = InternalDelegate;
            }
            // delegates[typeof(T)] = delegates.TryGetValue(typeof(T), out MessageDelegate tempDel)
            //     ? tempDel += InternalDelegate
            //     : InternalDelegate;

            return this;
        }

        public Messenger RemoveListener<T> (MessageDelegate<T> del) where T : Msg {
            if (!delegateLookup.TryGetValue(del, out MessageDelegate internalDelegate)) {
                return this;
            }
            
            if (delegates.TryGetValue(typeof(T), out MessageDelegate tempDel)) {
                tempDel -= internalDelegate;
                if (tempDel == null) {
                    delegates.Remove(typeof(T));
                } else {
                    delegates[typeof(T)] = tempDel;
                }
            }

            delegateLookup.Remove(del);
            predicates.Remove(typeof(T));

            return this;
        }

        public Messenger Dispatch(Msg msg) {
            if (!delegates.TryGetValue(msg.GetType(), out MessageDelegate del)) {
                return this;
            }
            
            bool invoke = true;
            if (predicates.TryGetValue(msg.GetType(), out MessagePredicate predicate)) {
                invoke = predicate(msg);
            }
            if (invoke) {
                del.Invoke(msg);
            }

            return this;
        }
    }
}