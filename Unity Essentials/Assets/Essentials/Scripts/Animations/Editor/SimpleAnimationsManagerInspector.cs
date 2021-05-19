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
        private SimpleAnimationsManager simpleAnimationsManager;
        
        
        public override void OnInspectorGUI()
        {
            
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update ();
            
            
            
            
            //specify type
            simpleAnimationsManager = target as SimpleAnimationsManager;
            if (simpleAnimationsManager == null) { return; }
            
            //find all implementations of ISimpleAnimation using System.Reflection.Module
            if (implementations == null)
                implementations = Utils.GetTypeImplementationsNotUnityObject<ISimpleAnimation>();

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
            ShowAnimationsArray(serializedObject.FindProperty("animations"));

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
                implementations = Utils.GetTypeImplementationsNotUnityObject<ISimpleAnimation>();
            }
            EditorGUILayout.EndHorizontal();
            
            
            
            
            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties ();
        }

        
        
        
        
        private void ShowAnimationsArray(UnityEditor.SerializedProperty list)
        {
            UnityEditor.EditorGUI.indentLevel += 1;
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.Space();
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    SerializedProperty transformProp = list.GetArrayElementAtIndex(i);

                    SimpleAnimation animation = ((SimpleAnimation) simpleAnimationsManager.animations[i]);
                    
                    string itemName;
                    if (animation.name.IsNullOrEmpty())
                        itemName = $"Animation [{i}]";
                    else
                        itemName = $"'{animation.name}' animation [{i}]";

                    /*string animType = animation.GetType().Name;
                    int charSize = 57;
                    int reaminingChars = charSize - animType.Length - itemName.Length;
                    for (int j = 0; j < reaminingChars; j++)
                        itemName += " ";
                    itemName += animType;*/
                    EditorGUILayout.PropertyField(transformProp, new GUIContent(itemName), true);

                    DisplayPreview(animation);

                    EditorGUILayout.Space();
                }
                EditorGUILayout.Space();
                
            }
            UnityEditor.EditorGUI.indentLevel -= 1;
        }
        private void DisplayPreview(SimpleAnimation animation)
        {

            UnityEngine.Object animatedObject = animation.GetAnimatedObject(false);
            if (animatedObject == null)
                return;
            int oldIndentLevel = UnityEditor.EditorGUI.indentLevel;
            UnityEditor.EditorGUI.indentLevel = 1;
            EditorGUILayout.BeginHorizontal();
            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 75;
            EditorGUILayout.LabelField("Animation preview");
            EditorGUIUtility.labelWidth = oldLabelWidth;
            EditorGUI.BeginChangeCheck();
            float animProgression = animation.progress;
            // Glitch to fix: The slider is not set to the proper value by default (after opening the scene, all the sliders are at 0, not at the proper value)
            //animProgression = EditorGUILayout.Slider(animProgression, animation.mirror?1f:0f, animation.mirror?0f:1f);
            animProgression = EditorGUILayout.Slider(animProgression, 0f, 1f);
            if (EditorGUI.EndChangeCheck())
            {
                if (animatedObject != null)
                {
                    Undo.RecordObject(animatedObject, "Changed animation progress (Animated Object)");
                }
                Undo.RecordObject(simpleAnimationsManager, "Changed animation progress (Simple Animation Manager)");
                animation.SetProgress(animProgression);
            }
            EditorGUILayout.EndHorizontal();
            UnityEditor.EditorGUI.indentLevel = oldIndentLevel;
        }
    }
}

#endif