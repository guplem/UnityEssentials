using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace UnityEssentials
{

    public class SuppressionOfWarningCS0649 : MonoBehaviour
    {
        /// <summary>
        /// Disables the warning CS0649.
        /// </summary>
        [MenuItem("Unity Essentials/Warning CS0649/Disable")]
        public static void DisableWarning()
        {
            File.WriteAllText("Assets/csc.rsp", "#\"This file disables the warning 'CS0649: Field 'var' is never assigned to, and will always have its default value null.'\"\n-nowarn:0649");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 disabled.");
        }

        /// <summary>
        /// Enables the warning CS0649.
        /// </summary>
        [MenuItem("Unity Essentials/Warning CS0649/Enable")]
        public static void EnableWarning()
        {
            File.Delete("Assets/csc.rsp.meta");
            File.Delete("Assets/csc.rsp");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 enabled.");
        }
    }

}
#endif