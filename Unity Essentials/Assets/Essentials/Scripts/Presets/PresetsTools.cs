#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace Essentials.Scripts.Presets
{
    public static class PresetsTools
    {
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
    
    
        //[MenuItem("CONTEXT/Hierarchy/Search mismatches between scene GameObjects and default Presets")]
        [MenuItem("GameObject/Presets/Search mismatches between scene GameObjects and default Presets", false)]
        public static void ValidateGameObjectsInSceneAgainstDefaultPresets()
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
        
            if (!foundAnyMissmatch)
                Debug.Log("All GameObjects' components in the scene are configured according to the selected preset.");
        
        }
    }
}
#endif