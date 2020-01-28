using UnityEditor;

public static class MiscEditorTools {
    
    [MenuItem("Tools/Misc/Repair ProjectSettings serialization")]
    public static void RepairPlayerSettingsSerialization() {
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }
}
