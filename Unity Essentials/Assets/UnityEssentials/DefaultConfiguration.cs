using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UnityEssentials
{
    public class DefaultConfiguration : MonoBehaviour
    {
        [MenuItem("Unity Essentials/Settings/Apply recommended configuration")]
        public static void ApplyRecommendedConfiguration()
        {
            ConsoleFeatures.Clear();
            Debug.Log("Applying recommended configuration...");
            
            QuickSearch.Install();
            SuppressionOfWarningCS0649.DisableWarning();
        }
        
        [MenuItem("Unity Essentials/Settings/Apply Unity's default configuration")]
        public static void RestoreDefaultConfiguration()
        {
            ConsoleFeatures.Clear();
            Debug.Log("Applying Unity's default configuration...");
            
            QuickSearch.Uninstall();
            SuppressionOfWarningCS0649.EnableWarning();
        }
    }
}
