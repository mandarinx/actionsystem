using UnityEngine;
using UnityEditor;

namespace Altruist {

    [CustomEditor(typeof(Inventory))]
    public class InventoryInspector : Editor {

        private Item itemToAdd;
        
        public override void OnInspectorGUI() {
            Inventory inventory = target as Inventory;

            EditorGUILayout.Space();

            if (EditorApplication.isPlaying) {
                using (var h = new EditorGUILayout.HorizontalScope()) {
                    itemToAdd = EditorGUILayout.ObjectField(itemToAdd, typeof(Item), true) as Item;
                    bool isPrefab = false;

                    if (itemToAdd != null) {
                        PrefabAssetType prefabAssetType = PrefabUtility.GetPrefabAssetType(itemToAdd);
                        isPrefab = prefabAssetType == PrefabAssetType.Regular ||
                                   prefabAssetType == PrefabAssetType.Variant;
                    }

                    if (GUILayout.Button("Add")) {
                        if (itemToAdd != null) {
                            if (isPrefab) {
                                Object instance = PrefabUtility.InstantiatePrefab(itemToAdd);
                                GameObject go = PrefabUtility.GetOutermostPrefabInstanceRoot(instance);
                                Inventory.Add(inventory, go.GetComponent<Item>());
                            } else {
                                Inventory.Add(inventory, itemToAdd);
                            }
                        }
                    }
                }

                EditorGUILayout.Space();
            }

            for (int i = 0; i < Inventory.Count(inventory); ++i) {
                EditorGUILayout.LabelField(new GUIContent(Inventory.Get(inventory, i).name));
            }
        }
    }
}