﻿#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Essentials.EssentialsSettings
{
    [ExecuteInEditMode]
    public class SettingsWindow : EditorWindow
    {
        
        [InitializeOnLoadMethod]
        private static void OpenWindowAtFirstUse()
        {
            //Check if is running tests
            if (!InternalEditorUtility.isHumanControllingUs || InternalEditorUtility.inBatchMode)
                return;

            if (!EssentialsSettings.settingsShown)
            {
                EditorApplication.delayCall += () =>
                {
                    OpenWindow();
                    EssentialsSettings.settingsShown = true;
                };
            }

        }

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Essentials/Settings")]
        static void OpenWindow()
        {
            // Get existing open window or if none, make a new one:
            SettingsWindow window = CreateWindow<SettingsWindow>();
            //SettingsWindow window = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow), false, "Essentials' Settings and Modifications");
            var windowSize = new Vector2(600f, 420f);
            window.minSize = window.maxSize = windowSize;
            window.position = Utils.GetEditorWindowCenteredPosition(windowSize);
            window.titleContent = new GUIContent("Essentials' Settings and Modifications");
            window.Show();
            window.Focus();
        }
        
        private Type[] implementations;
        void OnGUI()
        {
            if (implementations == null || implementations.Length == 0)
                SearchConfigurationModifiers();

            GUILayout.Label("Essentials Settings", EditorStyles.boldLabel);
            GUILayout.Label("");

            if (implementations != null && implementations.Length != 0)
            {
                
                GUILayout.Label("Recommended modifications:");
                
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.BeginVertical();
                foreach (var modificationType in implementations)
                {
                    IModification modification = (IModification) Activator.CreateInstance(modificationType);
                    if (!modification.showInSettingsWindow)
                        continue;
                    GUILayout.Label($" • {modification.title}");
                }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical();
                foreach (var modificationType in implementations)
                {
                    
                    IModification modification = (IModification) Activator.CreateInstance(modificationType);
                    if (!modification.showInSettingsWindow)
                        continue;
                    
                    EditorGUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button(new GUIContent(modification.infoButtonText, $"Open {modification.infoURL}")))
                        modification.OpenInfoURL();
                    GUILayout.Label(""); //Separation
                    GUILayout.Label(""); //Separation
                    
                    if (GUILayout.Button(new GUIContent(modification.applyButtonText, modification.applyModificationShortEplanation)))
                        modification.Apply();
                    if (GUILayout.Button(new GUIContent(modification.revertButtonText, modification.revertModificationShortEplanation)))
                        modification.Revert();

                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
                
                GUILayout.Label("");


                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Apply all"))
                {
                    foreach (var modificationType in implementations)
                    {
                        IModification modification = (IModification) Activator.CreateInstance(modificationType);
                        modification.Apply();
                    }
                }

                if (GUILayout.Button("Revert all"))
                {
                    foreach (var modificationType in implementations)
                    {
                        IModification modification = (IModification) Activator.CreateInstance(modificationType);
                        modification.Revert();
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
            
            #region ModificationsSearch
            EditorGUILayout.BeginHorizontal();
            if (implementations != null) EditorGUILayout.LabelField($"Found {implementations.Count()} modifications", EditorStyles.helpBox);
            if (implementations == null) EditorGUILayout.LabelField($"NO IMPLEMENTATIONS FOUND");
            if (GUILayout.Button(new GUIContent("Search for modifications", "Search for any implementation of the abstract class 'Modification' to be displayed in this window." )) && implementations == null)
                SearchConfigurationModifiers();
            EditorGUILayout.EndHorizontal();
            #endregion
            
            GUILayout.Label("");
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            
            #region Links
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Rate the asset! ❤️", "Open the Asset Store page of the asset, so you can rate it, share it or give any kind of love you want!" )))
                Application.OpenURL("https://assetstore.unity.com/packages/slug/161141");
            if (GUILayout.Button(new GUIContent("Give feedback or any ideas! 💡", "Open a form to share any thoughts you have about the asset, so we can keep improving." )))
                Application.OpenURL("https://forms.gle/diuUu6nZHAf5T67C9");
            if (GUILayout.Button(new GUIContent("About me  : )", "Open my personal webpage where you can know more about me!" )))
                Application.OpenURL("https://TriunityStudios.com");
            EditorGUILayout.EndHorizontal();
            #endregion
        }

        /// <summary>
        /// Find all implementations of IConfigurationModifier using System.Reflection.Module
        /// </summary>
        private void SearchConfigurationModifiers()
        {
            implementations = Utils.GetTypeImplementationsNotUnityObject<IModification>();
        }
    }
}

#endif