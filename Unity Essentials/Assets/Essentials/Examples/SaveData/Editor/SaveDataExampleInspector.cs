using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SaveDataExample))]
public class SaveDataExampleInspector : Editor
{
    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update ();
        
        base.OnInspectorGUI();
        
        SaveDataExample saveDataExample = (SaveDataExample)target;

        if (GUILayout.Button("Save all"))
        {
            saveDataExample.SaveAll();
        }
        
        if (GUILayout.Button("Load all"))
        {
            saveDataExample.LoadAll();
        }
        
        if (GUILayout.Button("Open folder"))
        {
            saveDataExample.OpenSavedDataFolder();
        }
        
        if (GUILayout.Button("Delete float file"))
        {
            saveDataExample.DeleteFloat();
        }
        
        if (GUILayout.Button("Delete all"))
        {
            saveDataExample.DeleteAll();
        }
        
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties ();
    }

}
#endif