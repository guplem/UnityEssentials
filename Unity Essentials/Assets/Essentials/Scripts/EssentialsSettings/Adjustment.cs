using UnityEngine;

namespace Essentials.EssentialsSettings
{
    /// <summary>
    /// Basic functionality for any adjustment that can be managed in the "Essentials Settings Window"
    /// </summary>
    public abstract class Adjustment : IAdjustment
    {
        
        /// <summary>
        /// Title or short definition of the configuration modification
        /// </summary>
        public abstract string title { get; }
    
        /// <summary>
        /// Applies the desired modification.
        /// </summary>
        public abstract void Apply();

        /// <summary>
        /// Reverts the modification leaving the state of the platform as it was before applying it.
        /// </summary>
        public abstract void Revert();

        /// <summary>
        /// Short explanation of what will be the actions done while applying the modification
        /// </summary>
        /// 
        public virtual string applyAdjustmentShortExplanation { get => title + " (apply)"; }
        /// <summary>
        /// Short explanation of what will be the actions done while reverting the modification
        /// </summary>
        public virtual string revertAdjustmentShortExplanation { get => title + " (revert)"; }

        /// <summary>
        /// Text displayed on the button to apply the modification
        /// </summary>
        public abstract string applyButtonText { get; }
        /// <summary>
        /// Text displayed on the button to revert the modification
        /// </summary>
        public abstract string revertButtonText { get; }
        
        /// <summary>
        /// Text displayed on the button to get more information about the modification
        /// </summary>
        public string infoButtonText => "Info";
        /// <summary>
        /// URL where the information regarding the modification can be found
        /// </summary>
        public abstract string infoURL { get; }
        
        /// <summary>
        /// If the modification should be displayed or not in the Essentials' settings window
        /// </summary>
        public virtual bool showInSettingsWindow => true;
        
        /// <summary>
        /// Opens the URL containing the information of the modification
        /// </summary>
        public void OpenInfoURL() { Application.OpenURL(infoURL); }

    }
}
