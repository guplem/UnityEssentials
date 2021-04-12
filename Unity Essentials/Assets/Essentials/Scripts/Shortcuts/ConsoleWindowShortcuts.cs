using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

#if UNITY_EDITOR
namespace Essentials.Shortcuts
{
    public class ConsoleWindowShortcuts : MonoBehaviour
    {
        /// <summary>
        /// Clears the Unity Editor's Console from all messages
        /// </summary>
        [Shortcut("Clear Console", KeyCode.Space, ShortcutModifiers.Action)]
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