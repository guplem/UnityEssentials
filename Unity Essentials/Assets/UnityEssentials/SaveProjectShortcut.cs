using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveProjectShortcut : MonoBehaviour
{
    [MenuItem("File/Save project %&s")]
    static void FunctionForceSaveProyect()
    {
        EditorApplication.ExecuteMenuItem("File/Save Project");
        Debug.Log("Saved project");
    }
}
