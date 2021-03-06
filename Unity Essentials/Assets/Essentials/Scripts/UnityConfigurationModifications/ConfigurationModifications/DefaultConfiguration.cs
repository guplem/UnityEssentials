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
        public static void ApplyRecommendedConfiguration()
        {
            EditorConsole.Clear();
            Debug.Log("Applying recommended configuration...");
            
            // Actions performed when the button "Apply recommended configuration" is clicked
            new QuickSearch().Apply();
            new SuppressionOfWarningCS0649().Apply();
        }
        
        /// <summary>
        /// Restores the configuration given by default by Unity on those aspects modified by the 'Apply recommended configuration' action.
        /// </summary>
        public static void RestoreDefaultConfiguration()
        {
            EditorConsole.Clear();
            Debug.Log("Applying Unity's default configuration...");
            
            // Actions performed when the button "Apply Unity's default configuration" is clicked
            new QuickSearch().Revert();
            new SuppressionOfWarningCS0649().Revert();
        }
    }
}
#endif