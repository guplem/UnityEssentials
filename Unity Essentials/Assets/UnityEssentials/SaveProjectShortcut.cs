using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    public class SaveProjectShortcut : MonoBehaviour
    {
        [MenuItem("File/Save project %&s")]
        static void FunctionForceSaveProyect()
        {
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved project");
        }
    }
}
