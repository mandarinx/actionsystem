using System;
using UnityEngine;
using System.Collections.Generic;

namespace Altruist {

    // internal class PropertyEvents {
    //
    //     private class EventHandler {
    //         public readonly Type propType;
    //         public readonly Item item;
    //
    //         public EventHandler(Type propType, Item item) {
    //             this.propType = propType;
    //             this.item = item;
    //         }
    //     }
    //     
    //     private readonly Dictionary<int, List<EventHandler>> addHandlers = new Dictionary<int, List<EventHandler>>();
    //     private readonly Dictionary<int, List<EventHandler>> removeHandlers = new Dictionary<int, List<EventHandler>>();
    //     // swapHandlers
    //
    //     internal static void RegisterAddEvent<T>(PropertyEvents pe,
    //                                              Item           source,
    //                                              Item           handler) where T : Component, IProperty {
    //         Register(pe.addHandlers, 
    //                  source.GetInstanceID(), 
    //                  new EventHandler(typeof(T), handler));
    //     }
    //
    //     internal static void RegisterRemoveEvent(PropertyEvents pe,
    //                                              Item           source,
    //                                              IProperty      prop,
    //                                              Item           handler) {
    //         Register(pe.removeHandlers, 
    //                  source.GetInstanceID(), 
    //                  new EventHandler(prop.GetType(), handler));
    //     }
    //
    //     internal static void DispatchAddEvents(PropertyEvents events,
    //                                            Item           source,
    //                                            IProperty      prop) {
    //         Dispatch(events.addHandlers, source, prop.GetType());
    //     }
    //
    //     internal static void DispatchRemoveEvents(PropertyEvents events,
    //                                               Item           source,
    //                                               IProperty      prop) {
    //         Dispatch(events.removeHandlers, source, prop.GetType());
    //     }
    //
    //     internal static bool DeregisterAddEventHandler(PropertyEvents events,
    //                                                    Item           source,
    //                                                    IProperty      prop,
    //                                                    Item           handler) {
    //         return Deregister(events.addHandlers, source, prop.GetType(), handler);
    //     }
    //
    //     internal static bool DeregisterRemoveEventHandler(PropertyEvents events,
    //                                                       Item           source,
    //                                                       IProperty      prop,
    //                                                       Item           handler) {
    //         return Deregister(events.removeHandlers, source, prop.GetType(), handler);
    //     }
    //
    //     private static bool Deregister(Dictionary<int, List<EventHandler>> handlerTable,
    //                                    Item           source,
    //                                    Type           propType,
    //                                    Item           handler) {
    //         int sourceId = source.GetInstanceID();
    //         
    //         if (!handlerTable.TryGetValue(sourceId, out List<EventHandler> handlers)) {
    //             return false;
    //         }
    //
    //         for (int i = handlers.Count - 1; i >= 0; --i) {
    //             if (handlers[i].propType == propType
    //                 && handlers[i].item == handler) {
    //                 handlers.RemoveAt(i);
    //                 return true;
    //             }
    //         }
    //
    //         return false;
    //     }
    //
    //     private static void Dispatch(Dictionary<int, List<EventHandler>> handlerTable,
    //                                  Item                                source,
    //                                  Type                                propType) {
    //         if (!handlerTable.TryGetValue(source.GetInstanceID(), 
    //                                       out List<EventHandler> handlers)) {
    //             // Debug.Log($"No registered ADD handler for item {source.name} and property {propType}");
    //             return;
    //         }
    //
    //         for (int i = 0; i < handlers.Count; ++i) {
    //             EventHandler handler = handlers[i];
    //             if (handler.propType != propType) {
    //                 continue;
    //             }
    //             ActionSystem.Resolve(handler.item, handler.item);
    //         }
    //     }
    //
    //     private static void Register(Dictionary<int, List<EventHandler>> handlerTable,
    //                                  int                                 sourceId,
    //                                  EventHandler                        handler) {
    //
    //         if (handlerTable.TryGetValue(sourceId, out List<EventHandler> handlers)) {
    //             if (!handlers.Contains(handler)) {
    //                 handlers.Add(handler);
    //             }
    //             return;
    //         }
    //
    //         handlers = new List<EventHandler> { handler };
    //         handlerTable.Add(sourceId, handlers);
    //     }
    // }
}