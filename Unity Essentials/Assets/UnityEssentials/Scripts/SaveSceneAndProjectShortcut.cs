using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    public class SaveSceneAndProjectShortcut : MonoBehaviour
    {
        [MenuItem("File/Save Scene And Project %#&s")]
        static void FunctionForceSaveSceneAndProyect()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved scene and project");
        }
    }
}
