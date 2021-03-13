using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Essentials
{
    public static class Utils
    {
        /// <summary>
        /// Find all implementations of the given parameter type except from those that are a subclass of 'UnityEngine.Object'.
        /// </summary>
        /// <typeparam name="T">The type from whom all the implementations are.</typeparam>
        /// <returns></returns>
        public static Type[] GetTypeImplementationsNotUnityObject<T>()
        {
            return GetTypeImplementations<T>().Where(impl=>!impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
        }
        
        /// <summary>
        /// Find all implementations of the given parameter type.
        /// </summary>
        /// <typeparam name="T">The type from whom all the implementations are.</typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypeImplementations<T>()
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

            Type interfaceType = typeof(T);
            return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
        }

    #region GetMainWindowCenteredPosition

        /// <summary>
        /// Returns a Rect containing the configuration to center a window.
        /// </summary>
        /// <param name="windowSize">Size of the window that wants to be centered.</param>
        /// <returns></returns>
        public static Rect GetWindowCenteredPosition(Vector2 windowSize)
        {
            Rect mainWindowRect = GetEditorMainWindowPos();
            return GetCenteredWindowPosition(mainWindowRect, windowSize);
        }

        private static UnityEngine.Object sMainWindow = null;
        private static Rect GetEditorMainWindowPos()
        {
            if (sMainWindow == null)
            {
                var containerWinType = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(ScriptableObject)).FirstOrDefault(t => t.Name == "ContainerWindow");
                if (containerWinType == null)
                    throw new MissingMemberException("Can't find internal type ContainerWindow. Maybe something has changed inside Unity");
                var showModeField = containerWinType.GetField("m_ShowMode", BindingFlags.NonPublic | BindingFlags.Instance);
                if (showModeField == null)
                    throw new MissingFieldException("Can't find internal fields 'm_ShowMode'. Maybe something has changed inside Unity");
                var windows = Resources.FindObjectsOfTypeAll(containerWinType);
                foreach (var win in windows)
                {
                    int showMode = (int)showModeField.GetValue(win);
                    if (showMode == 4) // main window
                    {
                        sMainWindow = win;
                        break;
                    }
                }
            }

            if (sMainWindow == null)
                return new Rect(0, 0, 800, 600);

            var positionProperty = sMainWindow.GetType().GetProperty("position", BindingFlags.Public | BindingFlags.Instance);
            if (positionProperty == null)
                throw new MissingFieldException("Can't find internal fields 'position'. Maybe something has changed inside Unity.");
            return (Rect)positionProperty.GetValue(sMainWindow, null);
        }
        
        private static Type[] GetAllDerivedTypes(this AppDomain aAppDomain, Type aType)
        {
            return TypeCache.GetTypesDerivedFrom(aType).ToArray();
        }
        
        private static Rect GetCenteredWindowPosition(Rect parentWindowPosition, Vector2 size)
        {
            var pos = new Rect
            {
                x = 0, y = 0,
                width = Mathf.Min(size.x, parentWindowPosition.width * 0.90f),
                height = Mathf.Min(size.y, parentWindowPosition.height * 0.90f)
            };
            float w = (parentWindowPosition.width - pos.width) * 0.5f;
            float h = (parentWindowPosition.height - pos.height) * 0.5f;
            pos.x = parentWindowPosition.x + w;
            pos.y = parentWindowPosition.y + h;
            return pos;
        }

    #endregion
        
    }
}
