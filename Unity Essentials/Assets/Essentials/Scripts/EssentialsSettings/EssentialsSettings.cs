using System.Collections.Generic;
using UnityEngine;

namespace Essentials.EssentialsSettings
{
    static class EssentialsSettings
    {

        #region WelcomeSettingsScreen
        
            private static bool _settingsShown;
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
            
        #endregion

    }
}
