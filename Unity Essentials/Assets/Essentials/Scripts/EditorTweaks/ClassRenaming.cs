#if UNITY_EDITOR
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Essentials.EditorTweaks
{
    public class ClassRenaming : MonoBehaviour
    {
        private static MonoScript[] scripts;
        
        [MenuItem("Assets/Update class name to match file name", false, 19)]
        public static void Fix()
        {
            if (scripts == null || scripts.Length != 1)
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
        public static bool CheckIfScriptFile()
        {
            // enable the menu item only if a script file is selected
            scripts = Selection.GetFiltered<MonoScript>(SelectionMode.Assets);

            // Only one selected
            return scripts.Length == 1;
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
    }
}
#endif