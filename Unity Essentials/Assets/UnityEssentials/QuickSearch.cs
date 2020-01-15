using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UnityEssentials
{
    public class QuickSearch : MonoBehaviour
    {
        [MenuItem("Unity Essentials/Quick Search/Install")]
        public static void Install()
        {
            Client.Add("com.unity.quicksearch");
            Debug.Log("Installing Quick Search...");
        }
        
        [MenuItem("Unity Essentials/Quick Search/Uninstall")]
        public static void Uninstall()
        {
            Client.Remove("com.unity.quicksearch");
            Debug.Log("Uninstalling Quick Search...");
        }
    }
}
