using System;
using UnityEngine;

namespace RL {
    
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionSystemAttribute : PropertyAttribute {
    
        public readonly Type action;
        public readonly Type property;
        // add string name to make it possible to
        // create a serialized item config

        public ActionSystemAttribute(Type actionType, Type propertyType) {
            action = actionType;
            property = propertyType;
        }
    }
}
