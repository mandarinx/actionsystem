#define LOG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Debug = UnityEngine.Debug;

// Future features:
// - cooldown. Configurable per action. Needs to be hooked up to the game loop somehow.

namespace RL {

    public delegate void ActionSystemAdded(IActionSystem actionSys);
    
    public class ActionSystem {
        
        private struct ActionData {
            public Type actionSystemType;
            public Type actionType;
            public Type propertyType;
        }

        private          Dictionary<Type, IActionSystem> actionSystems;
        private          Dictionary<Type, Type>          propertyTypes; // maps action types to property types
        private readonly string[]                        assemblies;
        private static   ActionSystem                    self;
        
        public event ActionSystemAdded OnActionSystemAdded = _ => {};

        public ActionSystem(params string[] assemblyNames) {
            self = this;
            assemblies = new string[assemblyNames.Length];
            Array.Copy(assemblyNames, assemblies, assemblyNames.Length);
        }
        
        public void RegisterSystems() {
            actionSystems = new Dictionary<Type, IActionSystem>();
            propertyTypes = new Dictionary<Type, Type>();
            List<ActionData> actionDatas = new List<ActionData>();

            for (int i = 0; i < assemblies.Length; ++i) {
                GetActionSystemsFromAssembly(Assembly.Load(assemblies[i]), 
                                             actionDatas);
            }

            int numDatas = actionDatas.Count;
            for (int i=0; i<numDatas; ++i) {
                ActionData data = actionDatas[i];
                propertyTypes.Add(data.actionType, data.propertyType);
                IActionSystem instance = (IActionSystem) Activator.CreateInstance(data.actionSystemType);
                actionSystems.Add(data.actionType, instance);
                OnActionSystemAdded(instance);
                Log($"Registered ActionSystem {data.actionSystemType} for action {data.actionType}");
            }

            // int numPlugins = systems.Count;
            // for (int i = 0; i < numPlugins; ++i) {
            //     systems[i].Ready();
            // }
        }

        private static bool HasSystemFor(Type actionType) {
            return self.actionSystems.ContainsKey(actionType);
        }

        private static IActionSystem GetSystem(Type actionType) {
            return self.actionSystems[actionType];
        }

        public static bool Resolve(Item source, IAction action, Item target) {
            Type actionType = action.GetType();
            
            if (!HasSystemFor(actionType)) {
                Debug.Log($"ActionSystem.Resolve : Has no system for {actionType}");
                return false;
            }

            if (!TryGetResolvedProperty(target, action, out _)) {
                Debug.Log($"ActionSystem.Resolve : "+
                          $"Target has no properties that matches the "+
                          $"action {actionType} of source {source}.");
                return false;
            }
            
            GetSystem(actionType).Resolve(source, action, target);
            return true;
        }

        private static bool TryGetResolvedProperty(Item target, IAction action, out IProperty property) {
            Type actionType = action.GetType();

            if (!self.propertyTypes.TryGetValue(actionType, out Type propertyType)) {
                property = default;
                return false;
            }

            IProperty[] properties = Property.Get(target);
            int pi = -1;

            for (int i = 0; i < properties.Length; ++i) {
                // if (propertyType.IsInstanceOfType(properties[i])) { // Permissive
                if (propertyType == properties[i].GetType()) { // Strict
                    pi = i;
                    break;
                }
            }
            
            // Debug.Log($"Target {target.name} has {properties.Length} properties, and the resolved one is at {pi}");
            property = pi >= 0 ? properties[pi] : default;
            return pi >= 0;
        }

        private static void GetActionSystemsFromAssembly(Assembly assembly, List<ActionData> cache) {
            Type[] types    = assembly.GetTypes();
            int    numTypes = types.Length;

            for (int i=0; i<numTypes; ++i) {
                Type type = types[i];

                if (Registered(cache, type)) {
                    continue;
                }

                if (type.IsAbstract) {
                    continue;
                }

                if (!TypeUtils.Implements<IActionSystem>(type)) {
                    continue;
                }

                ActionSystemAttribute attrib = GetAttribute(type);
                if (attrib == null) {
                    continue;
                }

                if (!TypeUtils.Implements<IAction>(attrib.action)) {
                    Debug.LogError($"ActionSystem {type.Name} tries to register action {attrib.action.Name} "+
                                   "which doesn't implement the IAction interface. Cannot register action system.");
                    continue;
                }

                cache.Add(new ActionData {
                    actionSystemType = type,
                    actionType       = attrib.action,
                    propertyType     = attrib.property
                });
            }
        }
        
        private static bool Registered(List<ActionData> data, Type type) {
            for (int i = 0, l = data.Count; i < l; i++) {
                if (data[i].actionSystemType == type) {
                    return true;
                }
            }
            return false;
        }

        private static ActionSystemAttribute GetAttribute(Type type) {
            return (ActionSystemAttribute)Attribute.GetCustomAttribute(type, 
                                                                       typeof(ActionSystemAttribute));
        }

        [Conditional("LOG")]
        private static void Log(object msg) {
            Debug.Log(msg);
        }
    }
}