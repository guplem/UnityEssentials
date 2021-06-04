using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace Essentials.EssentialsSettings.UnityConfigurationModifications
{

    /// <summary>
    /// Adjustment that forces the application of the presets to all assets contained in the same folder of the preset.
    /// </summary>
    public class EnforcePresetPerFolder : Adjustment
    {
        /// <summary>
        /// Enforce preset per folder renaming a file named EnforcePresetPostProcessor to EnforcePresetPostProcessor.cs in the Essentials/Presets folder.
        /// </summary>
        public override void Apply()
        {
            AssetDatabase.Refresh();
            
            string[] foundAssets = AssetDatabase.FindAssets("EnforcePresetPostProcessor.cs");
            if (foundAssets == null || foundAssets.Length < 1)
                Debug.LogError("No files named 'EnforcePresetPostProcessor.cs...' have been found.");
            else if (foundAssets.Length > 1)
                Debug.LogError("More than one file named 'EnforcePresetPostProcessor.cs...' has been found.");
            else
            {
                var path = AssetDatabase.GUIDToAssetPath(foundAssets[0]);
                File.Move (path, path.Remove(path.Length-4, 4));
                File.Delete(path+".meta");
                AssetDatabase.Refresh();
                
                Debug.Log("Presets are now enforced at the folder where they are present.");
                
                Popup.Init();
            }

        }

        /// <summary>
        /// Disables the enforcement of the presets per folder renaming a file named EnforcePresetPostProcessor.cs to EnforcePresetPostProcessor in the Essentials/Presets folder.
        /// </summary>
        public override void Revert()
        {
            string[] foundAssets = AssetDatabase.FindAssets("t:script EnforcePresetPostProcessor");
            if (foundAssets == null || foundAssets.Length < 1)
                Debug.LogError("No files named 'EnforcePresetPostProcessor...' have been found.");
            else if (foundAssets.Length > 1)
                Debug.LogError("More than one file named 'EnforcePresetPostProcessor...' has been found.");
            else
            {
                string path = AssetDatabase.GUIDToAssetPath(foundAssets[0]);
                File.Move(path, path + ".txt");
                File.Delete(path + ".meta");
                AssetDatabase.Refresh();
                
                Debug.Log("Enforcement of presets by folder disabled.");
            }
        }

        public override string title { get => "Enforce Preset Per Folder"; }
        public override string revertButtonText { get => "Revert"; }
        public override string infoURL { get => "https://docs.unity3d.com/Manual/DefaultPresetsByFolder.html"; }
        public override string applyButtonText { get => "Apply"; }

        public override string applyAdjustmentShortExplanation { get => "Applies the presets to all assets contained in the same folder of the preset."; }
        public override string revertAdjustmentShortExplanation { get => "Presets will no longer be automatically applied to the assets in the same folder of the preset."; }

        public override bool showInSettingsWindow => true;
        
        private class Popup : EditorWindow
        {
            public static void Init()
            {
                Popup window = ScriptableObject.CreateInstance<Popup>();
                Vector2 windowSize = new Vector2(250f, 150f);
                window.minSize = window.maxSize = windowSize;
                window.position = Utils.GetEditorWindowCenteredPosition(windowSize);
                window.ShowPopup();
            }

            void OnGUI()
            {
                EditorGUILayout.LabelField("If you want your already-imported Assets to adopt the configuration in the preset in the same folder, reimport them by right-clicking on the name of the asset.", EditorStyles.wordWrappedLabel);
                GUILayout.Space(60);
                if (GUILayout.Button("Okay")) this.Close();
            }
        }
    }

}
#endif
