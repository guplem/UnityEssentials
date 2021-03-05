using UnityEditor;
using UnityEngine;

namespace Essentials.QuickSetup
{
    public class SettingsWindow : EditorWindow
    {
        /*string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;*/

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
        }
    }
}
