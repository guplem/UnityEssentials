using UnityEngine;

namespace Essentials.EssentialsSettings
{
    /// <summary>
    /// Data related to the "Essentials Settings Window"
    /// </summary>
    static class SavedData
    {

        #region WelcomeSettingsScreen
        
            /// <summary>
            /// Whether the settings window has been shown or not for this user.
            /// </summary>
            public static bool settingsShown
            {
                get
                {
                    _settingsShown = PlayerPrefs.GetInt(nameof(settingsShown))==1;
                    return _settingsShown;
                }
                set
                {
                    if (_settingsShown == value) 
                        return;
                    _settingsShown = value;
                    PlayerPrefs.SetInt(nameof(settingsShown), value? 1 : 0);
                }
            }
            private static bool _settingsShown;
            
        #endregion

    }
}
