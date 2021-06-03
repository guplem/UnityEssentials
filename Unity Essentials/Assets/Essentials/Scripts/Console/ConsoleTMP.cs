using System;
using TMPro;

namespace UnityEngine
{
    /// <summary>
    /// Component to display the console (debug logs) in-game using a TMP_Text component
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class ConsoleTMP : Console
    {
        /// <summary>
        /// The TMP_Text component that is going to be updated to display all the log messages.
        /// </summary>
        [NonSerialized] public TMP_Text textTMP;

        protected override void UpdateVisuals()
        {
            if (textTMP == null)
                textTMP = gameObject.GetComponentRequired<TMP_Text>();

            textTMP.enabled = show;

            if (!show)
                return;

            textTMP.text = fullLog;
        }

    }
}