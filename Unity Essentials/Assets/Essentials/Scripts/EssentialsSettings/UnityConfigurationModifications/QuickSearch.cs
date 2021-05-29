#if UNITY_EDITOR
using UnityEditor.PackageManager;
using UnityEngine;

namespace Essentials.EssentialsSettings.UnityConfigurationModifications
{

    public class QuickSearch : Adjustment
    {
        public override bool showInSettingsWindow { get => false; }
        public override string title { get => "Install Quick Search"; }
        public override string applyButtonText { get => "Add dependency"; }
        public override string revertButtonText { get => "Remove dependency"; }
        public override string infoURL { get => "https://docs.unity3d.com/Packages/com.unity.quicksearch@1.1/manual/index.html"; }

        /// <summary>
        /// Install the Quick Search package adding a dependency in the project.
        /// </summary>
        public override void Apply()
        {
            Client.Add("com.unity.quicksearch");
            Debug.Log("Installing Quick Search...");
        }
        
        /// <summary>
        /// Uninstall the Quick Search package by removing a dependency in the project.
        /// </summary>
        public override void Revert()
        {
            Client.Remove("com.unity.quicksearch");
            Debug.Log("Uninstalling Quick Search...");
        }
        
        public override string applyAdjustmentShortExplanation { get => "Install the Quick Search package adding a dependency in the project."; }
        public override string revertAdjustmentShortExplanation { get => "Uninstall the Quick Search package by removing a dependency in the project."; }

        
    }

}
#endif