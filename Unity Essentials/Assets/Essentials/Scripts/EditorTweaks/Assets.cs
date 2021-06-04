#if UNITY_EDITOR
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Essentials.EditorTweaks
{
    /// <summary>
    /// Collection of tweaks to add features related to the management of assets in the editor
    /// </summary>
    public class Assets
    {
        
        #region ClassRenaming

        // Original author of the code: Pellegrino ~thp~ Principe (https://github.com/thp1972)
        // Original code: https://github.com/thp1972/MyUnityScripts/blob/master/FixClassName/Editor/com.pellegrinoprincipe/FixClassName.cs
        // The code has been modified to increase consistency, usability and to reduce possible errors.
        
        /// <summary>
        /// Updates the name of the class in the selected script to match the name of the script's file. 
        /// </summary>
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

        /// <summary>
        /// Checks if the selection only contains one script file.
        /// </summary>
        /// <returns>True if a single script is selected. False otherwise.</returns>
        [MenuItem("Assets/Update class name to match file name", true, 19)]
        private static bool IsSingleScriptSelected()
        {
            // enable the menu item only if a script file is selected
            MonoScript[] scripts = Selection.GetFiltered<MonoScript>(SelectionMode.Assets);

            // Only one selected
            return !scripts.IsNullOrEmpty() && scripts.Length == 1;
        }

        /// <summary>
        /// Replaces the name of the class int he given file with the given name.
        /// </summary>
        /// <param name="newClassName">The new desired name for the class.</param>
        /// <param name="scriptPath">The path to the script file containing the class.</param>
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

        /// <summary>
        /// Calls the Duplicate event to duplicate the selected elements.
        /// </summary>
        [MenuItem("Assets/Duplicate", false, 19)] // No shortcut is set because the functionality calls the Command "Edit/Duplicate" that already has cone signed
        private static void DuplicateAsset()
        {
            EditorWindow.focusedWindow.SendEvent (EditorGUIUtility.CommandEvent ("Duplicate"));
        }

        /// <summary>
        /// Checks if assets are currently being selected.
        /// </summary>
        /// <returns>Ture if one or more assets are selected. False otherwise.</returns>
        [MenuItem("Assets/Duplicate", true, 19)] // No shortcut is set because the functionality calls the Command "Edit/Duplicate" that already has cone signed
        private static bool AreAssetsSelected()
        {
            return Selection.assetGUIDs.Length > 0;
        } 

        #endregion
        
    }
}
#endif