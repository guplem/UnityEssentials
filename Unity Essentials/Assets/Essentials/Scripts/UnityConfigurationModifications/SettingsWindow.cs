using System;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Essentials.QuickSetup
{
    public class SettingsWindow : EditorWindow
    {
        /*string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;*/

        private Type[] implementations;
        
        // Add menu named "My Window" to the Window menu
        [MenuItem("Essentials/Settings")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            SettingsWindow window = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
            window.Show();
        }

        void OnGUI()
        {
            /*GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);

            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();*/
            
            GUILayout.Label("Essentials Settings", EditorStyles.boldLabel);
            GUILayout.Label("");

            if (implementations != null && implementations.Length != 0)
            {
                GUILayout.Label("Apply configuration modifications:");
                foreach (var modificationType in implementations)
                {
                    IConfigurationModifier configurationModifier = (IConfigurationModifier) Activator.CreateInstance(modificationType);
                    
                    EditorGUILayout.BeginHorizontal();
                        GUILayout.Label(configurationModifier.title);
                        if (GUILayout.Button(configurationModifier.applyButtonText))
                            configurationModifier.Apply();
                        if (GUILayout.Button(configurationModifier.revertButtonText))
                            configurationModifier.Revert();
                    EditorGUILayout.EndHorizontal();
                    
                }
                GUILayout.Label("");
            }












            EditorGUILayout.BeginHorizontal();
            if (implementations != null) EditorGUILayout.LabelField($"Found {implementations.Count()} configuration modifier");
            if (implementations == null) EditorGUILayout.LabelField($"NO IMPLEMENTATIONS FOUND");
            if (GUILayout.Button("Search for configuration modifiers") && implementations == null)
            {
                //find all implementations of IConfigurationModifier using System.Reflection.Module
                implementations = Utils.GetTypeImplementationsNotUnityObject<IConfigurationModifier>();
            }
            EditorGUILayout.EndHorizontal();
            
        }
    }
}
