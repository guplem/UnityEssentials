using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Essentials
{
    public class DefaultConfiguration : MonoBehaviour
    {
        /// <summary>
        /// Modifies the editor and project configuration to improve usability, easy of use, ...
        /// </summary>
        [MenuItem("Essentials/Settings/Apply recommended configuration")]
        public static void ApplyRecommendedConfiguration()
        {
            EditorConsole.Clear();
            Debug.Log("Applying recommended configuration...");
            
            // Actions performed when the button "Apply recommended configuration" is clicked
            QuickSearch.Install();
            SuppressionOfWarningCS0649.DisableWarning();
        }
        
        /// <summary>
        /// Restores the configuration given by default by Unity on those aspects modified by the 'Apply recommended configuration' action.
        /// </summary>
        [MenuItem("Essentials/Settings/Apply Unity's default configuration")]
        public static void RestoreDefaultConfiguration()
        {
            EditorConsole.Clear();
            Debug.Log("Applying Unity's default configuration...");
            
            // Actions performed when the button "Apply Unity's default configuration" is clicked
            QuickSearch.Uninstall();
            SuppressionOfWarningCS0649.EnableWarning();
        }
    }
}
#endif