#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Essentials.EditorTweaks
{

    public class Save : MonoBehaviour
    {
        /// <summary>
        /// Saves the scene and the project.
        /// </summary>
        [MenuItem("File/Save Scene and Project %#&s", false, 180)]
        static void SaveSceneAndProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save");
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved scene and project");
        }
        
        /// <summary>
        /// Saves the project.
        /// </summary>
        [MenuItem("File/Save project %&s", false, 200)]
        static void SaveProject()
        {
            EditorApplication.ExecuteMenuItem("File/Save Project");
            Debug.Log("Saved project");
        }
    }

}
#endif