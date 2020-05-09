#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SimpleAnimationsManager))]
public class SimpleAnimationsManagerInspector : Editor
{
    private Type[] implementations;
    private int selectedImplementationIndex;

    public override void OnInspectorGUI()
    {
        //specify type
        SimpleAnimationsManager simpleAnimationsManager = target as SimpleAnimationsManager;
        if (simpleAnimationsManager == null) { return; }

        EditorGUILayout.BeginHorizontal();
        if (implementations != null) EditorGUILayout.LabelField($"Found {implementations.Count()} implementations");
        if (implementations == null || GUILayout.Button("Search implementations"))
        {
            //find all implementations of ISimpleAnimation using System.Reflection.Module
            implementations = GetImplementations<ISimpleAnimation>().Where(impl=>!impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
        }
        EditorGUILayout.EndHorizontal();
        
        // Draw horizontal line
        EditorGUILayout.Space(); EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUILayout.Space(); 
        
        //select an implementation from all found using an editor popup
        selectedImplementationIndex = EditorGUILayout.Popup(new GUIContent("Implementation"),
            selectedImplementationIndex, implementations.Select(impl => impl.FullName).ToArray());

        ISimpleAnimation newAnimation = null;
        if (GUILayout.Button("Create animation"))
        {
            //Create a new animation of the selected type
            newAnimation = (ISimpleAnimation) Activator.CreateInstance(implementations[selectedImplementationIndex]);
        }

        //If a new animation has been created...
        if (newAnimation != null)
        {
            //record the gameObject state to enable undo and prevent from exiting the scene without saving
            Undo.RegisterCompleteObjectUndo(target, "Added new animation");
            //add the new animation to the animation's list
            if (simpleAnimationsManager.animations == null)
                simpleAnimationsManager.animations = new List<ISimpleAnimation>();
            simpleAnimationsManager.animations.Add(newAnimation);
        }

        // Draw horizontal line
        EditorGUILayout.Space(); EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUILayout.Space(); 
        
        base.OnInspectorGUI();
    }
    
    private static Type[] GetImplementations<T>()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

        var interfaceType = typeof(T);
        return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
    }
}
#endif