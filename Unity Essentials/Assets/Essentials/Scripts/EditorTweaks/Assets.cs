#if UNITY_EDITOR
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Essentials.EditorTweaks
{
    public class Assets : MonoBehaviour
    {

        #region ClassRenaming

        // Original author of the code: Pellegrino Principe (https://github.com/thp1972)
        // Original code: https://github.com/thp1972/MyUnityScripts/blob/master/FixClassName/Editor/com.pellegrinoprincipe/FixClassName.cs
        // The code has been modified to increase consistency, usability and to reduce possible errors.
        
        [MenuItem("Assets/Update class name to match file name", false, 19)]
        private static void UpdateClassNameToMatchFile()
        {
            
            MonoScript[] scripts = Selection.GetFiltered<MonoScript>(SelectionMode.Assets);
            
            if (scripts.IsNullOrEmpty() || scripts.Length != 1)
            {
                Debug.LogError("One script file must be selected to update the class contained in it.");
                return;
            }
                
            MonoScript script = scripts[0];
            int selectedScriptID = script.GetInstanceID();
            
            string newClassName = script.name;

            ReplaceClassName(newClassName, AssetDatabase.GetAssetPath(selectedScriptID));
        }

        [MenuItem("Assets/Update class name to match file name", true, 19)]
        private static bool IsSingleScriptSelected()
        {
            // enable the menu item only if a script file is selected
            MonoScript[] scripts = Selection.GetFiltered<MonoScript>(SelectionMode.Assets);

            // Only one selected
            return !scripts.IsNullOrEmpty() && scripts.Length == 1;
        }

        private static void ReplaceClassName(string newClassName, string scriptPath)
        {
            try
            {
                string[] fileText = File.ReadAllLines(scriptPath);

                for (int i = 0; i < fileText.Length; i++)
                {
                    // make the refactoring only if the class name is different
                    if (!Regex.IsMatch(fileText[i], @"\bclass\b"))
                        continue;

                    if (Regex.IsMatch(fileText[i], "\\b" + newClassName + "\\b"))
                    {
                        Debug.Log($"Class in file '{scriptPath}' already has the same name as the file ('{newClassName}').");
                        return; // skip if the name is the same
                    }

                    // match the identifier of a class so it can be replaced by 'className'
                    // we use a Positive Lookbehind...
                    const string regexPattern = @"(?<=class )\w+";
                    fileText[i] = Regex.Replace(fileText[i], regexPattern, newClassName);
                    File.WriteAllLines(scriptPath, fileText);

                    AssetDatabase.Refresh(ImportAssetOptions.Default);
                    Debug.Log($"Class in file '{scriptPath}' has been successfully renamed to '{newClassName}'.");
                }
            }
            catch (Exception exception)
            {
                Debug.Log($"Something went wrong: {exception.Message}");
            }
        }
        
        #endregion

        #region DuplicateAsset

        [MenuItem("Assets/Duplicate", false, 19)] // No shortcut is set because the functionality calls the Command "Edit/Duplicate" that already has cone signed
        private static void DuplicateAsset()
        {
            EditorWindow.focusedWindow.SendEvent (EditorGUIUtility.CommandEvent ("Duplicate"));
        }

        [MenuItem("Assets/Duplicate", true, 19)] // No shortcut is set because the functionality calls the Command "Edit/Duplicate" that already has cone signed
        private static bool AreAssetsSelected()
        {
            return Selection.assetGUIDs.Length > 0;
        } 

        #endregion
        
    }
}
#endif