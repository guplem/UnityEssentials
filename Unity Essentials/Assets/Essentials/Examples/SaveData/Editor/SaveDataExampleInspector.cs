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
        
        SaveDataExample myTarget = (SaveDataExample)target;

        if (GUILayout.Button("Save"))
        {
            myTarget.Save();
        }
        
        if (GUILayout.Button("Load"))
        {
            myTarget.Load();
        }
        
        if (GUILayout.Button("Open folder"))
        {
            SavedDataManager.OpenSavedDataFolder();
        }
    }

}
