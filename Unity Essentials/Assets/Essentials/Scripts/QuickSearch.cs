#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;


namespace Essentials
{

    public class QuickSearch : MonoBehaviour
    {
        /// <summary>
        /// Installs the Quick Search package (adds a dependency in the project).
        /// </summary>
        [MenuItem("Essentials/Quick Search/Install")]
        public static void Install()
        {
            Client.Add("com.unity.quicksearch");
            Debug.Log("Installing Quick Search...");
        }
        
        /// <summary>
        /// Uninstalls the Quick Search package.
        /// </summary>
        [MenuItem("Essentials/Quick Search/Uninstall")]
        public static void Uninstall()
        {
            Client.Remove("com.unity.quicksearch");
            Debug.Log("Uninstalling Quick Search...");
        }
    }

}
#endif