using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

public static class CheckVariationsInScene
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
            if (!go.activeInHierarchy)
                continue;

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
}
