using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using Object = UnityEngine.Object;

namespace RL {

    [Serializable]
    public class ActionConfig : Object {
        public string actionName;
        public string propName;
    }
    
    // This needs a project specific settings
    // - namespace
    // - file paths
    
    
    public class CreateActions : EditorWindow {
        
        private List<ActionConfig> actionConfigs;
        private const string tab = "    ";
        
        [MenuItem("Tools/RL/Create Actions")]
        public static void Init() {
            CreateActions win = GetWindow<CreateActions>();
            win.titleContent = new GUIContent("Create Actions");
            win.Show();
        }

        private void OnEnable() {
            actionConfigs = new List<ActionConfig>();
        }
        
        public void OnGUI() {
            using (var h = new EditorGUILayout.HorizontalScope()) {
                EditorGUILayout.LabelField(new GUIContent("Action"));
                EditorGUILayout.LabelField(new GUIContent("Property"));
            }
            
            EditorGUILayout.Space();

            int d = -1;
            
            for (int i = 0; i < actionConfigs.Count; ++i) {
                using (var h = new EditorGUILayout.HorizontalScope()) {
                    ActionConfig ac = actionConfigs[i];
                    ac.actionName = EditorGUILayout.TextField(ac.actionName);
                    ac.propName = EditorGUILayout.TextField(ac.propName);
                    if (GUILayout.Button("-", GUILayout.Width(30))) {
                        d = i;
                    }
                }
            }

            if (d >= 0) {
                actionConfigs.RemoveAt(d);
            }
            
            EditorGUILayout.Space();

            if (GUILayout.Button("Add", GUILayout.Width(40))) {
                actionConfigs.Add(new ActionConfig());
            }

            if (GUILayout.Button("Create files")) {
                CreateFiles(actionConfigs);
                AssetDatabase.Refresh();
            }
        }

        private static void CreateFiles(IReadOnlyList<ActionConfig> actionConfigs) {
            for (int i = 0; i < actionConfigs.Count; ++i) {
                string aname = Capitalize(actionConfigs[i].actionName);
                string pname = actionConfigs[i].propName == "IProperty" 
                    ? "IProperty"
                    : Capitalize(actionConfigs[i].propName);
                CreateAction(aname, $"{Application.dataPath}/Scripts/Game/Actions/{aname}Action.cs");
                CreateSystem(aname, pname, $"{Application.dataPath}/Scripts/Game/Actions/{aname}System.cs");
                CreateProperty(pname, $"{Application.dataPath}/Scripts/Game/Properties/Prop{pname}.cs");
            }
        }
        
        private static void CreateAction(string actionName, string fullPath) {
            if (File.Exists(fullPath)) {
                Debug.Log($"Cannot create Action {actionName} because the file {fullPath} already exists");
                return;
            }

            StringBuilder c = new StringBuilder();
            c.AppendLine("using UnityEngine;");
            c.AppendLine("using Altruist;");
            c.AppendLine("");
            c.AppendLine("namespace RL {");
            c.AppendLine("");
            c.AppendLine($"{tab}[AddComponentMenu(\"Actions/{actionName}\")]");
            c.AppendLine($"{tab}public class {actionName}Action : MonoBehaviour, IAction {{}}");
            c.AppendLine("}");

            WriteFile(fullPath, c.ToString());
        }
        
        private static void CreateSystem(string actionName, string propName, string fullPath) {
            if (File.Exists(fullPath)) {
                Debug.Log($"Cannot create System {actionName} because the file {fullPath} already exists");
                return;
            }

            propName = propName == "IProperty" ? "IProperty" : $"Prop{propName}";

            StringBuilder c = new StringBuilder();
            c.AppendLine("using System;");
            c.AppendLine("using System.Collections;");
            c.AppendLine("using UnityEngine;");
            c.AppendLine("using Altruist;");
            c.AppendLine("using JetBrains.Annotations;");
            c.AppendLine("");
            c.AppendLine("namespace RL {");
            c.AppendLine("");
            c.AppendLine($"{tab}[UsedImplicitly]");
            c.AppendLine($"{tab}[ActionSystem(typeof({actionName}Action))]");
            c.AppendLine($"{tab}public class {actionName}System : IActionSystem {{");
            c.AppendLine("");
            c.AppendLine($"{tab}{tab}public Type TargetProperty => typeof({propName});");
            c.AppendLine("");
            c.AppendLine($"{tab}{tab}public IEnumerator Resolve(Item source, IAction sourceAction, Item target, IProperty targetProp) {{");
            c.AppendLine($"{tab}{tab}{tab}yield break;");
            c.AppendLine($"{tab}{tab}}}");
            c.AppendLine($"{tab}}}");
            c.AppendLine("}");

            WriteFile(fullPath, c.ToString());
        }
        
        private static void CreateProperty(string propName, string fullPath) {
            if (propName == "IProperty") {
                return;
            }

            if (File.Exists(fullPath)) {
                Debug.Log($"Cannot create Property {propName} because the file {fullPath} already exists");
                return;
            }

            StringBuilder c = new StringBuilder();
            c.AppendLine("using UnityEngine;");
            c.AppendLine("using Altruist;");
            c.AppendLine("");
            c.AppendLine("namespace RL {");
            c.AppendLine("");
            c.AppendLine($"{tab}[AddComponentMenu(\"Properties/{propName}\")]");
            c.AppendLine($"{tab}public class Prop{propName} : MonoBehaviour, IProperty {{}}");
            c.AppendLine("}");

            WriteFile(fullPath, c.ToString());
        }
        
        private static void WriteFile(string fullPath, string content) {
            File.WriteAllText(fullPath, content, Encoding.UTF8);
            Debug.Log($"Wrote file to {fullPath}");
        }

        public static string Capitalize(string str) {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
    }
}