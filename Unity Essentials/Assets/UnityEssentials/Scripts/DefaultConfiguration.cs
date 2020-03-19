using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    #if UNITY_EDITOR
    public class DefaultConfiguration : MonoBehaviour
    {
        [MenuItem("Unity Essentials/Settings/Apply recommended configuration")]
        public static void ApplyRecommendedConfiguration()
        {
            ConsoleFeatures.Clear();
            Debug.Log("Applying recommended configuration...");
            
            // Actions performed when the button "Apply recommended configuration" is clicked
            QuickSearch.Install();
            SuppressionOfWarningCS0649.DisableWarning();
        }
        
        
        [MenuItem("Unity Essentials/Settings/Apply Unity's default configuration")]
        public static void RestoreDefaultConfiguration()
        {
            ConsoleFeatures.Clear();
            Debug.Log("Applying Unity's default configuration...");
            
            // Actions performed when the button "Apply Unity's default configuration" is clicked
            QuickSearch.Uninstall();
            SuppressionOfWarningCS0649.EnableWarning();
        }
    }
    #endif
}
