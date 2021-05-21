using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SaveDataExample))]
public class SaveDataExampleInspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        SaveDataExample saveDataExample = (SaveDataExample)target;

        if (GUILayout.Button("Save all"))
        {
            saveDataExample.Save();
        }
        
        if (GUILayout.Button("Load all"))
        {
            saveDataExample.Load();
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
    }

}
