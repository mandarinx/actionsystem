using System;
using System.Collections.Generic;

namespace Altruist {

    public class Bridge {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void Add(object service) {
            services.Add(service.GetType(), service);
        }

        public T Get<T>() where T : class {
            return (T)services[typeof(T)];
        }
    }
}