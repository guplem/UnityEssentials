using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
#if UNITY_EDITOR
    public class SuppressionOfWarningCS0649 : MonoBehaviour
    {
        [MenuItem("Unity Essentials/Warning CS0649/Disable")]
        public static void DisableWarning()
        {
            File.WriteAllText("Assets/csc.rsp", "#\"This file disables the warning 'CS0649: Field 'var' is never assigned to, and will always have its default value null.'\"\n-nowarn:0649");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 disabled.");
        }

    
        [MenuItem("Unity Essentials/Warning CS0649/Enable")]
        public static void EnableWarning()
        {
            File.Delete("Assets/csc.rsp.meta");
            File.Delete("Assets/csc.rsp");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 enabled.");
        }
    }
#endif
}
