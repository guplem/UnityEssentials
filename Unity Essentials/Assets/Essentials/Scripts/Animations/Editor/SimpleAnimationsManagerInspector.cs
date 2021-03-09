#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UnityEngine
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SimpleAnimationsManager))]
    public class SimpleAnimationsManagerInspector : UnityEditor.Editor
    {
        private Type[] implementations;
        private int selectedImplementationIndex;

        
        
        public override void OnInspectorGUI()
        {
            
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update ();
            
            
            
            
            //specify type
            SimpleAnimationsManager simpleAnimationsManager = target as SimpleAnimationsManager;
            if (simpleAnimationsManager == null) { return; }
            
            //find all implementations of ISimpleAnimation using System.Reflection.Module
            if (implementations == null)
                implementations = Essentials.Utils.GetTypeImplementationsNotUnityObject<ISimpleAnimation>();

            EditorGUILayout.Space();
            
            //select an implementation from all found using an editor popup
            selectedImplementationIndex = EditorGUILayout.Popup(new GUIContent("Animation type"),
                selectedImplementationIndex, implementations.Select(impl => impl.Name).ToArray());

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

            if (simpleAnimationsManager.animations != null)
            {
                for (int a = 0; a < simpleAnimationsManager.animations.Count; a++)
                {
                    if (simpleAnimationsManager.animations[a] == null)
                        EditorGUILayout.HelpBox("The animation with index " + a + " is null.\nRecommended to delete the array element by right clicking on it.", MessageType.Warning);
                
                    if (simpleAnimationsManager.animations.Count() != simpleAnimationsManager.animations.Distinct().Count())
                    {
                        for (int d = a+1; d < simpleAnimationsManager.animations.Count; d++)
                        {
                            if (simpleAnimationsManager.animations[a] != null && (simpleAnimationsManager.animations[a] == simpleAnimationsManager.animations[d]) )
                                EditorGUILayout.HelpBox("The animations with index " + a + " and " + d + " are the same object.", MessageType.Warning);
                        }
                    }
                }
            }
        
            EditorGUI.indentLevel += 1;
            EditorGUILayout.Space(); 
            GUILayout.Label("Animations Configuration", EditorStyles.boldLabel);
            //using (new GUILayout.VerticalScope(EditorStyles.helpBox)) {
                ShowArrayProperty(serializedObject.FindProperty("animations"));
            //}
            EditorGUI.indentLevel -= 1;
            
            // Draw horizontal line
            EditorGUILayout.Space(); EditorGUILayout.Space();  
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); 
            
            // Implementations search
            EditorGUILayout.BeginHorizontal();
            if (implementations != null) EditorGUILayout.LabelField($"Found {implementations.Count()} implementations", EditorStyles.helpBox);
            if (implementations == null || GUILayout.Button("Search implementations"))
            {
                //find all implementations of ISimpleAnimation using System.Reflection.Module
                implementations = Essentials.Utils.GetTypeImplementationsNotUnityObject<ISimpleAnimation>();
            }
            EditorGUILayout.EndHorizontal();
            
            
            
            
            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties ();
        }

        
        
        
        
        private void ShowArrayProperty(UnityEditor.SerializedProperty list)
        {
            UnityEditor.EditorGUI.indentLevel += 1;
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.Space();
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    //UnityEditor.EditorGUILayout.PropertyField (
                    //    list.GetArrayElementAtIndex(i),
                    //    new UnityEngine.GUIContent("Animation " + (i + 1).ToString())
                    //);
                    SerializedProperty transformProp = list.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(transformProp, new GUIContent("Animation " + i), true);
                    EditorGUILayout.Space();
                }
                EditorGUILayout.Space();
                
            }
            UnityEditor.EditorGUI.indentLevel -= 1;
        }
        
    }
}

#endif