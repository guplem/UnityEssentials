using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace Essentials.EssentialsSettings.UnityConfigurationModifications
{

    public class SuppressionOfWarningCS0649 : Adjustment
    {
        /// <summary>
        /// Disable the warning CS069 creating a file named csc.rsp in the project folder.
        /// </summary>
        public override void Apply()
        {
            File.WriteAllText("Assets/csc.rsp", "#\"This file disables the warning 'CS0649: Field 'var' is never assigned to, and will always have its default value null.'\"\n-nowarn:0649");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 disabled.");
        }

        /// <summary>
        /// Enables the warning CS069 by removing a file named csc.rsp in the project folder.
        /// </summary>
        public override void Revert()
        {
            File.Delete("Assets/csc.rsp.meta");
            File.Delete("Assets/csc.rsp");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 enabled.");
        }

        public override string title { get => "Disable warning CS0649"; }
        public override string revertButtonText { get => "Revert"; }
        public override string infoURL { get => "https://answers.unity.com/questions/60461/warning-cs0649-field-is-never-assigned-to-and-will.html"; }
        public override string applyButtonText { get => "Apply"; }

        public override string applyAdjustmentShortExplanation { get => "Disable the warning CS069 creating a file named csc.rsp in the project folder."; }
        public override string revertAdjustmentShortExplanation { get => "Enable the warning CS069 by removing a file named csc.rsp in the project folder."; }

        public override bool showInSettingsWindow => false;
    }

}
#endif