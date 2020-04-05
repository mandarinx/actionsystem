#define LOG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Debug = UnityEngine.Debug;

// Future features:
// - cooldown. Configurable per action. Needs to be hooked up to the game loop somehow.

namespace RL {

    // public delegate IEnumerator Resolver(Item      source,
    //                                      IAction   sourceAction,
    //                                      Item      target,
    //                                      Systems    systems);
    
    public class ActionSystem {
        
        private struct ActionData {
            public Type actionSystemType;
            public Type actionType;
        }

        private          Dictionary<Type, IActionSystem> actionSystems;
        private readonly string[]                        assemblies;
        private static   ActionSystem                    self;
        
        public ActionSystem(params string[] assemblyNames) {
            self = this;
            assemblies = new string[assemblyNames.Length];
            Array.Copy(assemblyNames, assemblies, assemblyNames.Length);
        }
        
        public void RegisterSystems() {
            actionSystems = new Dictionary<Type, IActionSystem>();
            List<ActionData> actionDatas = new List<ActionData>();

            for (int i = 0; i < assemblies.Length; ++i) {
                GetActionSystemsFromAssembly(Assembly.Load(assemblies[i]), 
                                             actionDatas);
            }

            int numDatas = actionDatas.Count;
            for (int i=0; i<numDatas; ++i) {
                ActionData data = actionDatas[i];
                actionSystems.Add(data.actionType, 
                                  (IActionSystem) Activator.CreateInstance(data.actionSystemType));
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

        // public static bool HasSystemFor<T>() where T : IAction {
        //     return self.systems.ContainsKey(typeof(T));
        // }

        private static IActionSystem GetSystem(Type actionType) {
            return self.actionSystems[actionType];
        }

        // private static IEnumerator[] GetResolversAsEnumerators(Item source, Item target) {
        //     IAction[]     actions   = Action.Get(source);
        //     IEnumerator[] resolvers = new IEnumerator[actions.Length];
        //
        //     for (int i = 0; i < actions.Length; ++i) {
        //         if (TryGetResolver(actions[i], target, out IProperty property, out Resolver resolver)) {
        //             resolvers[i] = resolver(source, actions[i], target, self.systems);
        //         }
        //     }
        //     
        //     return resolvers;
        // }

        // private static IEnumerator GetResolverAsEnumerators(Item source, IAction action, Item target) {
        //     return TryGetResolver(action, target, out IProperty property, out Resolver resolver) 
        //         ? resolver(source, action, target, self.systems) 
        //         : NullResolver(source, action, target, self.systems);
        // }

        // public static void Resolve(Item source, Item target, ActionRunner runner) {
        //     runner.AddEnumerators(GetResolversAsEnumerators(source, target));
        // }

        public static void Resolve(Item source, IAction action, Item target) {
            Type actionType = action.GetType();
            
            if (!HasSystemFor(actionType)) {
                Debug.Log($"ActionSystem.Resolve : Has no system for {actionType}");
                return;
            }

            if (!TryGetResolvedProperty(target, action, out _)) {
                Debug.Log($"ActionSystem.Resolve : "+
                          $"Target has no properties that matches the "+
                          $"action {actionType} of source {source}.");
                return;
            }
            
            GetSystem(actionType).Resolve(source, action, target);
        }

        public static void Resolve(Item source, Item target) {
            // resolve all actions
        }

        public static bool CanResolveFor(IAction action, Item target) {
            return HasSystemFor(action.GetType()) && TryGetResolvedProperty(target, action, out _);
        }

        // private static bool TryGetResolver(IAction action, Item target, out IProperty property) {
        //     Type actionType = action.GetType();
        //         
        //     if (!HasSystemFor(actionType)) {
        //         Debug.Log($"ActionSystem has no system for {actionType}");
        //         property = null;
        //         return false;
        //     }
        //
        //     return TryGetResolvedProperty(target, action, out property);
        // }

        private static bool TryGetResolvedProperty(Item target, IAction action, out IProperty property) {
            Type          actionType = action.GetType();
            IProperty[]   properties = Property.Get(target);
            int           pi         = -1;
            IActionSystem system     = GetSystem(actionType);

            for (int i = 0; i < properties.Length; ++i) {
                // system.TargetProperty.IsInstanceOfType(properties[i]) // Permissive
                if (system.TargetProperty != properties[i].GetType()) { // Strict
                    continue;
                }
                pi = i;
                break;
            }
            
            // Debug.Log($"Target {target.name} has {properties.Length} properties, and the resolved one is at {pi}");
            property = pi < 0 ? default : properties[pi];
            return pi >= 0;
        }
        
        // As long as there is at least one property that matches the Action's
        // target property, the Action is considered resolvable
        // private static bool CanResolve(IAction action, IReadOnlyList<IProperty> properties) {
        //     Type actionType = action.GetType();
        //         
        //     if (!HasSystemFor(actionType)) {
        //         return false;
        //     }
        //
        //     IActionSystem system = Get(actionType);
        //     for (int i = 0; i < properties.Count; ++i) {
        //         return system.TargetProperty == properties[i].GetType(); // Strict
        //         // return system.TargetProperty.IsInstanceOfType(properties[i]); // Permissive
        //     }
        //
        //     return false;
        // }

        // // TODO: Seems like bad naming!
        // public static Type GetPropertyType<T>() where T : IAction {
        //     IActionSystem system = Get(typeof(T));
        //     return system.TargetProperty;
        // }

        // private static Resolver GetResolver(IAction action) {
        //     return (source, sourceAction, target, systems1) => 
        //         GetSystem(action.GetType()).Resolve(source, sourceAction, target, systems1);
        // }

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

        // private static IEnumerator NullResolver(Item      source,
        //                                         IAction   sourceAction,
        //                                         Item      target,
        //                                         Systems systems) {
        //     yield break;
        // }

        [Conditional("LOG")]
        private static void Log(object msg) {
            Debug.Log(msg);
        }
    }
}