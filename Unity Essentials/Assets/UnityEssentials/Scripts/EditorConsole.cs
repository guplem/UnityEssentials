using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace UnityEssentials
{
    public class EditorConsole : MonoBehaviour
    {
        /// <summary>
        /// Clears the Unity Editor's Console from all messages
        /// </summary>
        [MenuItem ("Unity Essentials/Console/Clear Console  %SPACE")] // Ctrl + Shift + Alt + C
        public static void Clear()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }

}
#endif