using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Essentials
{
    /// <summary>
    /// Useful information for the usage and understanding of the asset.
    /// </summary>
    public static class EssentialsHelp
    {
        /// <summary>
        /// Opens the web page of the asset in the Asset Store
        /// </summary>
        [MenuItem("Help/Essentials/Rate the asset! ❤️", false, 0)]
        public static void OpenLinkRateAsset(){
            Application.OpenURL("https://assetstore.unity.com/packages/slug/161141");
        }
        /// <summary>
        /// Opens a web page with a form to give feedback related to the asset
        /// </summary>
        [MenuItem("Help/Essentials/Give feedback or any ideas! 💡",false, 0)]
        public static void OpenLinkFeedback(){
            Application.OpenURL("https://forms.gle/diuUu6nZHAf5T67C9");
        }        
        
        
        /// <summary>
        /// Opens a web page containing the latest documentation available for the asset
        /// </summary>
        [MenuItem("Help/Essentials/Documentation",false, 50)]
        public static void OpenLinkDocumentation(){
            Application.OpenURL("https://docs.google.com/document/d/1-strmOzT7ka8uO8hEH_W3Xjlr-ajjKcJ0b6h5KYnpJQ/edit?usp=sharing");
        }
        /// <summary>
        /// Opens a web page containing the documentation related to the scripts of the asset
        /// </summary>
        [MenuItem("Help/Essentials/Scripting Documentation",false, 50)]
        public static void OpenLinkScriptingDocumentation(){
            Application.OpenURL("https://guplem.github.io/Essentials/html/");
        }
        
        /// <summary>
        /// Opens a web page containing the GitHub repository of the asset
        /// </summary>
        [MenuItem("Help/Essentials/Scripting Documentation",false, 50)]
        public static void OpenLinkGitHubRepository(){
            Application.OpenURL("https://github.com/guplem/UnityEssentials");
        }
        
        /// <summary>
        /// Opens the web page of the creator of the asset
        /// </summary>
        [MenuItem("Help/Essentials/About me  : )", false, 100)]
        public static void OpenLinkAboutMe(){
            Application.OpenURL("https://TriunityStudios.com");
        }
        
    }
}

#endif