using System;
using UnityEngine.UI;

namespace UnityEngine
{
    /// <summary>
    /// Component to display the console (debug logs) in-game using a Text component
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class ConsoleTextUI : Console
    {
        /// <summary>
        /// The Text component that is going to be updated to display all the log messages.
        /// </summary>
        [NonSerialized] public Text textUI;

        protected override void UpdateVisuals()
        {
            if (textUI == null)
                textUI = gameObject.GetComponentRequired<Text>();

            textUI.enabled = show;

            if (!show)
                return;
            textUI.text = fullLog;

        }

    }
}