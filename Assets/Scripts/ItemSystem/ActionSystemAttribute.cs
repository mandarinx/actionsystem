using System;
using UnityEngine;

namespace Altruist {
    
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionSystemAttribute : PropertyAttribute {
    
        public readonly Type action;
        // add string name to make it possible to
        // create a serialized item config

        public ActionSystemAttribute(Type actionType) {
            action = actionType;
        }
    }
}
