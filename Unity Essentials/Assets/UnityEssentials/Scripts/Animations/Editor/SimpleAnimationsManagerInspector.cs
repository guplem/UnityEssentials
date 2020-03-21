using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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

        if (GUILayout.Button("Create animation"))
        {
            //Create and add a new animation of the selected type
            simpleAnimationsManager.animations.Add((ISimpleAnimation) Activator.CreateInstance(implementations[selectedImplementationIndex]));
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
