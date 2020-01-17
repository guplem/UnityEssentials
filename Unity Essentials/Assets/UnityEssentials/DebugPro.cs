using System.Collections.Generic;

namespace UnityEngine
{
    public class DebugPro : Debug
    {
        public static void Log<T>(string message, IEnumerable<T> listToDebug, Object context = null)
        {
            string str = message + string.Join(", ", new List<T>(listToDebug).ConvertAll(i => i.ToString()).ToArray());
            if (context != null) Debug.Log(str, context);
            else Debug.Log(str);
        }
        
        public static void Log<T>(string message, T[] arrayToDebug, Object context = null)
        {
            string str = message + string.Join(", ", new List<T>(arrayToDebug).ConvertAll(i => i.ToString()).ToArray());
            if (context != null) Debug.Log(str, context);
            else Debug.Log(str);
        }
        
    }

}
