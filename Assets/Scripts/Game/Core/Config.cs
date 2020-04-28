using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace RL.Core {

    public class Config {

        [JsonProperty] private ConfigBlock[] configs;
        
        private Dictionary<Type, ConfigBlock> configTable = new Dictionary<Type, ConfigBlock>();

        public Config() {}
        
        public Config(params ConfigBlock[] configObjects) {
            configs = new ConfigBlock[configObjects.Length];
            Array.Copy(configObjects, configs, configObjects.Length);
        }
        
        public void Init() {
            for (int i = 0; i < configs.Length; ++i) {
                Debug.Log(configs[i].GetType());
                configTable.Add(configs[i].GetType(), configs[i]);
            }
        }
        
        public T Get<T>() where T : class {
            return configTable.TryGetValue(typeof(T), out ConfigBlock config) 
                ? config as T 
                : default;
        }

    }
}