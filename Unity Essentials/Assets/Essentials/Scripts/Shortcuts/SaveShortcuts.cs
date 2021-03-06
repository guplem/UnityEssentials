using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Essentials.Shortcuts
{

    public class SaveShortcuts : MonoBehaviour
    {
        /// <summary>
        /// Saves the project.
        /// </summary>
        [MenuItem("File/Save project %&s")]
        static void SaveProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved project");
        }
        
        /// <summary>
        /// Saves the scene and the project.
        /// </summary>
        [MenuItem("File/Save Scene and Project %#&s")]
        static void SaveSceneAndProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved scene and project");
        }
    }

}
#endif