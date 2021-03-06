﻿using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
namespace Essentials
{

    public class SuppressionOfWarningCS0649 : ConfigurationMofification
    {
        /// <summary>
        /// Disables the warning CS0649.
        /// </summary>
        public override void Apply()
        {
            File.WriteAllText("Assets/csc.rsp", "#\"This file disables the warning 'CS0649: Field 'var' is never assigned to, and will always have its default value null.'\"\n-nowarn:0649");
            AssetDatabase.Refresh();
        
            Debug.Log("Warning CS0649 disabled.");
        }

        /// <summary>
        /// Enables the warning CS0649.
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
        public override string applyButtonText { get => "Apply"; }
    }

}
#endif