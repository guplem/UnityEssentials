#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace Essentials.Presets
{
    /// <summary>
    /// Collection of tweaks to add features related to the use of presets
    /// </summary>
    public static class PresetsTools
    {
        /// <summary>
        /// Checks if all the GameObjects in the current scene are matching the selected (command) presets
        /// </summary>
        /// <param name="command">The context</param>
        [MenuItem("CONTEXT/Preset/Validate all Game Objects in scene")]
        public static void ValidateAllGameObjectsInScene(MenuCommand command)
        {
            // Get our current selected Preset.
            Preset referencePreset = command.context as Preset;

            if (referencePreset == null)
                return;

            bool foundAnyMissmatch = false;
        
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
            foreach (GameObject go in allObjects)
            {
                //if (!go.activeInHierarchy)
                //    continue;

                foreach (Component component in go.GetComponents(typeof(Component)))
                {
                    if (!referencePreset.CanBeAppliedTo(component))
                        continue;

                    if (!referencePreset.DataEquals(component))
                    {
                        Debug.LogWarning($"The '{referencePreset.GetTargetTypeName()}' in the Game Object '{go}' does not match the selected preset.", component);
                        foundAnyMissmatch = true;
                    }

                }

            }
        
            if (!foundAnyMissmatch)
                Debug.Log("All GameObjects' components in the scene are configured according to the selected preset.");
        }
        
        
        /// <summary>
        /// Checks if all the GameObjects in the current scene are matching the configuration of the default presets
        /// </summary>
        [MenuItem("GameObject/Presets/Search mismatches between scene GameObjects and default Presets", false, -20)]
        public static bool AreAllGameObjectsInSceneMatchingWithDefaultPresets()
        {
            bool foundAnyMissmatch = false;

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
            //Transform[] allObjectsTransforms = Selection.transforms;
            foreach (GameObject go in allObjects)
            {
                foreach (Component component in go.GetComponents(typeof(Component)))
                {
                    Preset[] defaults = Preset.GetDefaultPresetsForObject(component);

                    foreach (Preset defaultPreset in defaults)
                    {
                        // Debug.Log($"Checking {defaultPreset.name} against '{defaultPreset.GetTargetTypeName()}' Component of '{go}' GameObject");
                        if (!defaultPreset.CanBeAppliedTo(component))
                        {
                            continue;
                        }
                        else
                        {
                            if (!defaultPreset.DataEquals(component))
                            {
                                Debug.LogWarning($"The '{defaultPreset.GetTargetTypeName()}' in the Game Object '{go}' does not match the default preset ({defaultPreset.name}).", component);
                                foundAnyMissmatch = true;
                            }
                            break;
                        }
                    }
                }
            }

            if (foundAnyMissmatch)
                return false;
            
            Debug.Log("All GameObjects' components in the scene are configured according to the selected preset.");
            return true;
        }
    }
}
#endif