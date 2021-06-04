#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Essentials.EditorTweaks
{
    /// <summary>
    /// Tweaks to add features related to the console in the editor
    /// </summary>
    public class Console
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
            method?.Invoke(new object(), null);
        }
    }

}
#endif
