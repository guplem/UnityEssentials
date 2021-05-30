using System;

namespace UnityEngine
{
    /// <summary>
    /// Base class to handle the console display in-game
    /// </summary>
    public abstract class Console : MonoBehaviour
    {
        /// <summary>
        /// The log messages of the console.
        /// </summary>
        internal string fullLog = "";
        
        /// <summary>
        /// The filename that will store the logs of the console.
        /// </summary>
        private string filename = "";
        
        /// <summary>
        /// If the console must be displayed or not.
        /// </summary>
        [Tooltip("If the console must be displayed or not.")]
        [SerializeField] protected bool show = true;
        
        /// <summary>
        /// Should the console toggle on and off pressing the toggleKey?"
        /// </summary>
        [Tooltip("Should the console toggle on and off pressing the 'Toggle key'?")]
        [SerializeField] protected bool enableKeyToggle = true;
        
        /// <summary>
        /// Key to toggle on-off the in-game console.
        /// </summary>
        [Tooltip("Key to toggle on-off the in-game console.")]
        [SerializeField] public KeyCode toggleKey = KeyCode.Space;
        
        /// <summary>
        /// Should the logs be saved to a file in the desktop?
        /// </summary>
        [Tooltip("Should the logs be saved to a file in the desktop?")]
        [SerializeField] public bool saveToFile = false;
        
        /// <summary>
        /// Maximum amount of characters displayed by the console.
        /// </summary>
        [Tooltip("Maximum amount of characters displayed by the console.")]
        [SerializeField] public int maxDisplayedChars = 700;
        
        /// <summary>
        /// Registers the console to handle any log messages received and updates the visuals calling UpdateVisuals.
        /// </summary>
        protected void OnEnable() { Application.logMessageReceived += Log; UpdateVisuals(); }
        /// <summary>
        /// Unregisters the console from the handling of the any log messages.
        /// </summary>
        protected void OnDisable() { Application.logMessageReceived -= Log; }
        /// <summary>
        /// Updates the console's visuals calling UpdateVisuals
        /// </summary>
        protected void Awake() { UpdateVisuals(); }
        
        /// <summary>
        /// Inverts the "show" state of the console. If it was being displayed, it will no longer be. If it was not, it will be.
        /// </summary>
        public void ToggleShow()
        {
            SetShow(!show);
        }
        
        /// <summary>
        /// Displays or not the console in-game
        /// </summary>
        /// <param name="newValue">Weather the console should be displayed in-game or not.</param>
        public void SetShow(bool newValue)
        {
            show = newValue;
            UpdateVisuals();
        }

        /// <summary>
        /// Handles the toggling ON and OFF of the console
        /// </summary>
        private void Update()
        {
            if (enableKeyToggle && Input.GetKeyDown(toggleKey))
            {
                ToggleShow();
            }
        }

        /// <summary>
        /// Handles the logging of any log messages
        /// </summary>
        private void Log(string logString, string stackTrace, LogType type)
        {
            // Keep track of the full log
            fullLog = fullLog + "\n" + logString;
            if (fullLog.Length > maxDisplayedChars)
            {
                fullLog = fullLog.Substring(fullLog.Length - maxDisplayedChars);
            }
            

            // Save the log in a file
            if (saveToFile)
            {
                if (filename == "")
                {
                    string desktopFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Unity_Console_Logs";
                    System.IO.Directory.CreateDirectory(desktopFolder);
                    string date = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                    filename = desktopFolder + $"/log_{Utils.GetProjectName()}_{date}.txt";
                }
                try
                {
                    System.IO.File.AppendAllText(filename, logString + "\n\n");
                }
                catch (Exception e)
                {
                    Debug.LogError($"Console logs could not be saved:\n{e.Message}");
                }
            }
            
            UpdateVisuals();
        }

        /// <summary>
        /// Updates the visuals of the console
        /// </summary>
        protected abstract void UpdateVisuals();

        /// <summary>
        /// Clears the console and starts a new file for the saved logs.
        /// </summary>
        public void Clear()
        {
            fullLog = "";
            filename = "";
            UpdateVisuals();
        }
    }

}
