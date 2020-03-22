using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace UnityEssentials
{

    public class SavesShortcuts : MonoBehaviour
    {
        /// <summary>
        /// Saves the project.
        /// </summary>
        [MenuItem("File/Save project %&s")]
        static void ForceSaveProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved project");
        }
        
        /// <summary>
        /// Saves the scene and the project.
        /// </summary>
        [MenuItem("File/Save Scene And Project %#&s")]
        static void FunctionForceSaveSceneAndProyect()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved scene and project");
        }
    }

}
#endif