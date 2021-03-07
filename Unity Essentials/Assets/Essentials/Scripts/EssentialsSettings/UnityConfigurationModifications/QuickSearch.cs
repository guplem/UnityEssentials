#if UNITY_EDITOR
using UnityEditor.PackageManager;
using UnityEngine;

namespace Essentials.EssentialsSettings.UnityConfigurationModifications
{

    public class QuickSearch : Modification
    {
        public override string title { get => "Install Quick Search"; }
        public override string applyButtonText { get => "Apply"; }
        public override string revertButtonText { get => "Revert"; }

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
        
        public override string applyModificationShortEplanation { get => "Install the Quick Search package adding a dependency in the project."; }
        public override string revertModificationShortEplanation { get => "Uninstall the Quick Search package by removing a dependency in the project."; }

        
    }

}
#endif