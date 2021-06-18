// Original author of the code: Pellegrino ~thp~ Principe (https://github.com/thp1972)
// Original code: https://github.com/thp1972/MyUnityScripts/blob/master/GameObjectSeparator/Editor/com.pellegrinoprincipe/GOSeparators.cs
// The code has been modified to increase consistency, usability and to reduce possible errors.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Essentials.EditorTweaks
{
    /// <summary>
    /// Functionality to create consistent dividers in the editor's hierarchy
    /// </summary>
    public class HierarchyDividers : EditorWindow
    {

        #region DividerConfiguration

        /// <summary>
        /// The name that is going to be used in the divider GameObject
        /// </summary>
        private string sectionName = "";
        /// <summary>
        /// The selected character to build the "line" (-, * or _) that is going to be used alongside the section name to create the divider GameObject's name
        /// </summary>
        private int selectedCharNumber;
        /// <summary>
        /// The location of the name in relation with the "line" characters (left, center or right)
        /// </summary>
        private int selectedAlignmentNumber;
        /// <summary>
        /// The amount of characters used to create the "line" next to the section name
        /// </summary>
        private int numberOfDividerChars;

        #endregion
        
        #region predifined options

        /// <summary>
        /// The available characters to create the "line"
        /// </summary>
        private string[] availableChars = { "-", "*", "_" };
        /// <summary>
        /// The available locations of the name of the section relative to the line's characters
        /// </summary>
        private string[] alignText = { "Left", "Center", "Right" };

        #endregion

        #region EditorPrefs

        /// <summary>
        /// Name of the EditorPrefs storing numberOfDividerChars
        /// </summary>
        private string numberOfDividerCharsEditorPrefs = $"Essentials_{nameof(numberOfDividerChars)}";
        /// <summary>
        /// Name of the EditorPrefs storing numberOfDividerChars
        /// </summary>
        private string selectedCharNumberEditorPrefs = $"Essentials_{nameof(selectedCharNumber)}";
        /// <summary>
        /// Name of the EditorPrefs storing numberOfDividerChars
        /// </summary>
        private string selectedAlignmentNumberEditorPrefs = $"Essentials_{nameof(selectedAlignmentNumber)}";

        #endregion

        #region ButtonsStates

        /// <summary>
        /// Hs the button "ok" been pressed?
        /// </summary>
        private bool buttonStateOk;
        /// <summary>
        /// Hs the button "cancel" been pressed?
        /// </summary>
        private bool buttonStateCancel;

        #endregion

        /// <summary>
        /// Load preferences when the window is loaded
        /// </summary>
        private void OnEnable()
        {
            GetPreferences();
        }

        /// <summary>
        /// Draw the window and check the key presses
        /// </summary>
        private void OnGUI()
        {
            DrawWindow();
            ManageKeyPress();
        }
        
        /// <summary>
        /// Open the "Divider creator tool" window 
        /// </summary>
        [MenuItem("GameObject/Create Divider", false, -20)]
        private static void OpenWindow()
        {
            // Get existing open window or if none, make a new one:
            HierarchyDividers window = CreateWindow<HierarchyDividers>();
            //SettingsWindow window = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow), false, "Essentials' Settings and Adjustments");
            Vector2 windowSize = new Vector2(420f, 130f);
            window.minSize = window.maxSize = windowSize;
            window.position = Utils.GetEditorWindowCenteredPosition(windowSize);
            window.titleContent = new GUIContent("Divider creator tool");
            window.Show();
            window.Focus();
        }
        
        /// <summary>
        /// Draws the window
        /// </summary>
        private void DrawWindow()
        {
            EditorGUILayout.Space();
            
            
            // Section name
            GUILayout.BeginHorizontal();
            GUILayout.Label("Section name: ");
            sectionName = GUILayout.TextField(sectionName, 50, GUILayout.Width(236));
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            // Divider chars
            GUILayout.BeginHorizontal();
            GUILayout.Label("Divider char: ", GUILayout.Width(130));
            selectedCharNumber = GUILayout.SelectionGrid(selectedCharNumber, availableChars, availableChars.Length, EditorStyles.miniButton);
            GUILayout.EndHorizontal();

            // Alignment
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name position: ", GUILayout.Width(130));
            selectedAlignmentNumber = GUILayout.SelectionGrid(selectedAlignmentNumber, alignText, alignText.Length, EditorStyles.miniButton);
            GUILayout.EndHorizontal();
            
            // Char number
            GUILayout.BeginHorizontal();
            numberOfDividerChars = EditorGUILayout.IntSlider("Number of divider chars: ", numberOfDividerChars, 0, 80);
            GUILayout.EndHorizontal();

            // Horizontal line
            //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // buttons
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            buttonStateCancel = GUILayout.Button("Cancel", GUILayout.Width(80));
            buttonStateOk = GUILayout.Button("Create", GUILayout.Width(80));
            GUILayout.EndHorizontal();

            if (buttonStateOk)
            {
                AcceptCreation();
            }

            if (buttonStateCancel)
                Close();
        }
        
        /// <summary>
        /// Execute process of creating the divider with the current configuration
        /// </summary>
        private void AcceptCreation()
        {
            CreateDivider();
            SavePreferences();
            Close();
        }

        /// <summary>
        /// Loads the previous divider's preferences
        /// </summary>
        private void GetPreferences()
        {
            selectedCharNumber = EditorPrefs.GetInt(selectedCharNumberEditorPrefs, 0);
            selectedAlignmentNumber = EditorPrefs.GetInt(selectedAlignmentNumberEditorPrefs, 1);
            numberOfDividerChars = EditorPrefs.GetInt(numberOfDividerCharsEditorPrefs, 26);
        }

        /// <summary>
        /// Saves the current divider's prefrences
        /// </summary>
        private void SavePreferences()
        {
            EditorPrefs.SetInt(numberOfDividerCharsEditorPrefs, numberOfDividerChars);
            EditorPrefs.SetInt(selectedCharNumberEditorPrefs, selectedCharNumber);
            EditorPrefs.SetInt(selectedAlignmentNumberEditorPrefs, selectedAlignmentNumber);
        }

        /// <summary>
        /// Checks if the keys with related actions are being pressed. If so, close or create the divider.
        /// </summary>
        private void ManageKeyPress()
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.Escape:
                    Close();
                    break;
                case KeyCode.Return:
                    AcceptCreation();
                    break;
            }
        }

        /// <summary>
        /// Creates the divider by creating a GameObject in the scene with the name matching the desired configuration and the "EditorOnly" tag.
        /// </summary>
        private void CreateDivider()
        {
            string selectedChar = availableChars[selectedCharNumber];
            
            int charsLeft = 0, charsRight = 0;
            string dividerGameObjectName = "";
            
            switch (alignText[selectedAlignmentNumber])
            {
                case "Left":
                    charsLeft = 0;
                    charsRight = numberOfDividerChars;
                    break;
                case "Center":
                    charsLeft = numberOfDividerChars/2;
                    charsRight = numberOfDividerChars/2;
                    break;
                case "Right":
                    charsLeft = numberOfDividerChars;
                    charsRight = 0;
                    break;
            }

            for (int i = 0; i < charsLeft; i++)
                dividerGameObjectName += selectedChar;
            dividerGameObjectName += $" {sectionName} ";
            for (int i = 0; i < charsRight; i++)
                dividerGameObjectName += selectedChar;

            GameObject dividerGameObject = new GameObject(dividerGameObjectName);
            
            dividerGameObject.tag = "EditorOnly";
        }

    }
}
#endif