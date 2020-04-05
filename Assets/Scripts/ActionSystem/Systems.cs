using System;
using System.Collections.Generic;

namespace RL {

    public class Systems {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void Add(object service) {
            services.Add(service.GetType(), service);
        }

        public T Get<T>() where T : class {
            return (T)services[typeof(T)];
        }
    }
}