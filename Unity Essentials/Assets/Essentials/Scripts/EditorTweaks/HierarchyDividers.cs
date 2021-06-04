// Original author of the code: Pellegrino ~thp~ Principe (https://github.com/thp1972)
// Original code: 
// The code has been modified to increase consistency, usability and to reduce possible errors.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Essentials.EditorTweaks
{
    public class HierarchyDividers : EditorWindow
    {
        private string sectionName = "";
        private int selectedCharNumber;
        private int selectedAlignmentStateNumber;
        private int numberOfDividerChars;
        
        private string[] availableChars = { "-", "*", "_" };
        private string[] alignText = { "Left", "Center", "Right" };

        // prefs keys
        private string numberOfDividerCharsPlayerPrefs = "Essentials_" + nameof(numberOfDividerCharsPlayerPrefs);
        private string preferedCharPlayerPrefs = "Essentials_" + nameof(preferedCharPlayerPrefs);
        private string preferedAlignmentPlayerPrefs = "Essentials_" + nameof(preferedAlignmentPlayerPrefs);
        
        private bool buttonStateOk;
        private bool buttonStateCancel;

        private void OnEnable()
        {
            GetPreferences();
        }

        private void OnGUI()
        {
            CreateTheUI();
            ManageKeyPress();
        }
        
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
        
        private void CreateTheUI()
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
            selectedAlignmentStateNumber = GUILayout.SelectionGrid(selectedAlignmentStateNumber, alignText, alignText.Length, EditorStyles.miniButton);
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
        private void AcceptCreation()
        {
            CreateDivider();
            SavePreferences();
            Close();
        }

        private void GetPreferences()
        {
            selectedCharNumber = EditorPrefs.GetInt("Essentials_" + nameof(preferedCharPlayerPrefs), 0);
            selectedAlignmentStateNumber = EditorPrefs.GetInt("Essentials_" + nameof(preferedAlignmentPlayerPrefs), 1);
            numberOfDividerChars = EditorPrefs.GetInt("Essentials_" + nameof(numberOfDividerCharsPlayerPrefs), 26);
        }

        private void SavePreferences()
        {
            EditorPrefs.SetInt(preferedCharPlayerPrefs, selectedCharNumber);
            EditorPrefs.SetInt(preferedAlignmentPlayerPrefs, selectedAlignmentStateNumber);
        }

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

        private void CreateDivider()
        {
            string selectedChar = availableChars[selectedCharNumber];
            
            int charsLeft = 0, charsRight = 0;
            string dividerGameObjectName = "";
            
            switch (alignText[selectedAlignmentStateNumber])
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