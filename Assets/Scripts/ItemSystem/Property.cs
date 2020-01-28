using System;
using UnityEngine;

namespace Altruist {

    public static class Property {
        
        private static readonly PropertyEvents events = new PropertyEvents();

        public static T Add<T>(Item item) where T : Component, IProperty {
            T prop = item.GetComponent<T>();
            if (prop != null) {
                return prop;
            }
            prop = item.gameObject.AddComponent<T>();
            // Debug.Log($"Added property {typeof(T)} to {item.name}");
            PropertyEvents.DispatchAddEvents(events, item, prop);
            return prop;
        }

        public static bool Has<T>(Item item) where T : Component, IProperty {
            return item.GetComponent<T>() != null;
        }

        public static IProperty[] Get(Item item) {
            return item.GetComponents<IProperty>();
        }

        public static T Get<T>(Item item) where T : Component, IProperty {
            return item.GetComponent<T>();
        }

        public static Type[] GetTypes(Item item) {
            IProperty[] props = item.GetComponents<IProperty>();
            Type[] types = new Type[props.Length];
            
            for (int i = 0; i < types.Length; ++i) {
                types[i] = props[i].GetType();
            }

            return types;
        }

        public static void RegisterAddEvent<T>(Item source, Item handler) where T : Component, IProperty {
            PropertyEvents.RegisterAddEvent<T>(events, source, handler);
        }
    }
}