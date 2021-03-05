#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;


namespace Essentials
{

    public class QuickSearch : ConfigurationMofification
    {
        public override string title { get => "Install Quick Search"; }
        public override string applyButtonText { get => "Apply"; }
        public override string revertButtonText { get => "Revert"; }

        /// <summary>
        /// Installs the Quick Search package (adds a dependency in the project).
        /// </summary>
        public override void Apply()
        {
            Client.Add("com.unity.quicksearch");
            Debug.Log("Installing Quick Search...");
        }
        
        /// <summary>
        /// Uninstalls the Quick Search package.
        /// </summary>
        public override void Revert()
        {
            Client.Remove("com.unity.quicksearch");
            Debug.Log("Uninstalling Quick Search...");
        }
        
    }

}
#endif