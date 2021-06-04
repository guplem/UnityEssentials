#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Essentials.EssentialsSettings
{
    
    /// <summary>
    /// Window containing adjustments to the project and editor.
    /// <para>Additionally, it contains useful links related to the asset such as documentation.</para>>
    /// </summary>
    public class SettingsWindow : EditorWindow
    {
        
        /// <summary>
        /// Opens the window the first time the asset is running in a new editor.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void OpenWindowAtFirstUse()
        {
            //Check if is running tests
            if (!InternalEditorUtility.isHumanControllingUs || InternalEditorUtility.inBatchMode)
                return;

            if (!SavedData.settingsShown)
            {
                EditorApplication.delayCall += () =>
                {
                    OpenWindow();
                    SavedData.settingsShown = true;
                };
            }

        }
        
        /// <summary>
        /// Opens the "Essentials Settings" window
        /// </summary>
        [MenuItem("Window/Essentials/Settings")]
        private static void OpenWindow()
        {
            // Get existing open window or if none, make a new one:
            SettingsWindow window = CreateWindow<SettingsWindow>();
            //SettingsWindow window = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow), false, "Essentials' Settings and Adjustments");
            Vector2 windowSize = new Vector2(600f, 420f);
            window.minSize = window.maxSize = windowSize;
            window.position = Utils.GetEditorWindowCenteredPosition(windowSize);
            window.titleContent = new GUIContent("Essentials' Settings and Adjustments");
            window.Show();
            window.Focus();
        }
        
        /// <summary>
        /// List of types of adjustments
        /// </summary>
        private Type[] adjustments;
        
        /// <summary>
        /// Draws the window
        /// </summary>
        private void OnGUI()
        {
            if (adjustments == null || adjustments.Length == 0)
                SearchAdjustments();

            GUILayout.Label("Essentials Settings", EditorStyles.boldLabel);
            GUILayout.Label("");

            if (adjustments != null && adjustments.Length != 0)
            {
                
                GUILayout.Label("Available adjustments:");
                
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.BeginVertical();
                foreach (Type adjustmentType in adjustments)
                {
                    IAdjustment adjustment = (IAdjustment) Activator.CreateInstance(adjustmentType);
                    if (!adjustment.showInSettingsWindow)
                        continue;
                    GUILayout.Label($" • {adjustment.title}");
                }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical();
                foreach (Type adjustmentType in adjustments)
                {
                    
                    IAdjustment adjustment = (IAdjustment) Activator.CreateInstance(adjustmentType);
                    if (!adjustment.showInSettingsWindow)
                        continue;
                    
                    EditorGUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button(new GUIContent(adjustment.infoButtonText, $"Open {adjustment.infoURL}")))
                        adjustment.OpenInfoURL();
                    GUILayout.Label(""); //Separation
                    GUILayout.Label(""); //Separation
                    
                    if (GUILayout.Button(new GUIContent(adjustment.applyButtonText, adjustment.applyAdjustmentShortExplanation)))
                        adjustment.Apply();
                    if (GUILayout.Button(new GUIContent(adjustment.revertButtonText, adjustment.revertAdjustmentShortExplanation)))
                        adjustment.Revert();

                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
                
                GUILayout.Label("");


                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Apply all"))
                {
                    foreach (Type adjustmentType in adjustments)
                    {
                        IAdjustment adjustment = (IAdjustment) Activator.CreateInstance(adjustmentType);
                        adjustment.Apply();
                    }
                }

                if (GUILayout.Button("Revert all"))
                {
                    foreach (Type adjustmentType in adjustments)
                    {
                        IAdjustment adjustment = (IAdjustment) Activator.CreateInstance(adjustmentType);
                        adjustment.Revert();
                    }
                }
                EditorGUILayout.EndHorizontal();
                
                GUILayout.Label("");
                
            }
            
            GUILayout.Label("");
            
            #region Tooltip
                GUIStyle style = GUI.skin.label; //get the current style of the GUI;
                TextAnchor defaultAlignment = style.alignment; // Expected: TextAnchor.MiddleLeft
                style.alignment = TextAnchor.MiddleRight;
                GUILayout.Label (GUI.tooltip, style);
                style.alignment = defaultAlignment;
            #endregion
            
            /*
            EditorGUILayout.BeginHorizontal();
            if (implementations != null) EditorGUILayout.LabelField($"Found {implementations.Count()} adjustments", EditorStyles.helpBox);
            if (implementations == null) EditorGUILayout.LabelField($"NO IMPLEMENTATIONS FOUND");
            if (GUILayout.Button(new GUIContent("Search for adjustments", "Search for any implementation of the abstract class 'Adjustment' to be displayed in this window." )) && implementations == null)
                SearchConfigurationModifiers();
            EditorGUILayout.EndHorizontal();
            */
            
            GUILayout.Label("");
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            
            #region Links
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Rate the asset! ❤️", "Open the Asset Store page of the asset, so you can rate it, share it or give any kind of love you want!" )))
                EssentialsHelp.OpenLinkRateAsset();
            if (GUILayout.Button(new GUIContent("Give feedback or any ideas! 💡", "Open a form to share any thoughts you have about the asset, so we can keep improving." )))
                EssentialsHelp.OpenLinkFeedback();
            if (GUILayout.Button(new GUIContent("About me  : )", "Open my personal webpage where you can know more about me!" )))
                EssentialsHelp.OpenLinkAboutMe();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Quick Documentation", "Open the online document containing the quick documentation of the latest version of the asset." )))
                EssentialsHelp.OpenLinkDocumentation();
            if (GUILayout.Button(new GUIContent("Scripting Documentation", "Open the online scripting documentation of the latest version of the asset." )))
                EssentialsHelp.OpenLinkScriptingDocumentation();
            if (GUILayout.Button(new GUIContent("GitHub", "Open the GitHub repository of the asset. You are welcome to collaborate!" )))
                EssentialsHelp.OpenLinkGitHubRepository();
            EditorGUILayout.EndHorizontal();
            #endregion
        }

        /// <summary>
        /// Find all implementations of IConfigurationModifier using System.Reflection.Module
        /// </summary>
        private void SearchAdjustments()
        {
            adjustments = Utils.GetTypeImplementationsNotUnityObject<IAdjustment>();
        }
    }
}

#endif