using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
#if UNITY_EDITOR
    public class ConsoleFeatures : MonoBehaviour
    {
        [MenuItem ("Unity Essentials/Console/Clear Console  %SPACE")] // Ctrl + Shift + Alt + C
        public static void Clear()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }
#endif
}
