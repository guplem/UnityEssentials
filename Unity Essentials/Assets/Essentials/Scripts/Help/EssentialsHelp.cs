using UnityEditor;
using UnityEngine;

public static class EssentialsHelp
{
    [MenuItem("Help/Essentials/Rate the asset! ❤️")]
    public static void OpenLinkRateAsset(){
        Application.OpenURL("https://assetstore.unity.com/packages/slug/161141");
    }

    [MenuItem("Help/Essentials/Give feedback or any ideas! 💡")]
    public static void OpenLinkFeedback(){
        Application.OpenURL("https://forms.gle/diuUu6nZHAf5T67C9");
    }

    [MenuItem("Help/Essentials/About me  : )")]
    public static void OpenLinkAboutMe(){
        Application.OpenURL("https://TriunityStudios.com");
    }
}
