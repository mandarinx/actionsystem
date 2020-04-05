using System;

namespace RL {

    public static class TypeUtils {
        
        public static bool Implements<T>(Type type) {
            return typeof(T).IsAssignableFrom(type);
        }

    }
}