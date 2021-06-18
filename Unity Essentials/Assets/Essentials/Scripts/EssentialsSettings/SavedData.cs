using UnityEditor;

namespace Essentials.EssentialsSettings
{
    /// <summary>
    /// Stores data related to the "Essentials Settings Window"
    /// </summary>
    static class SavedData
    {

        #region WelcomeSettingsScreen
        
            #if UNITY_EDITOR
            /// <summary>
            /// Whether the settings window has been shown or not for this user.
            /// </summary>
            public static bool settingsShown
            {
                get
                {
                    _settingsShown = EditorPrefs.GetInt("Essentials_" + nameof(settingsShown))==1;
                    return _settingsShown;
                }
                set
                {
                    if (_settingsShown == value) 
                        return;
                    _settingsShown = value;
                    EditorPrefs.SetInt("Essentials_" + nameof(settingsShown), value? 1 : 0);
                }
            }
            private static bool _settingsShown;
            #endif
        
        #endregion

    }
}
