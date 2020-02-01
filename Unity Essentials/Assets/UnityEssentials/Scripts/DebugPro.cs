using System.Collections.Generic;

namespace UnityEngine
{
    public class DebugPro : Debug
    {
        public static void Log<T>(IEnumerable<T> listToDebug, string message, Object context = null)
        {
            string str = message;
            if (listToDebug != null)
                str = str + string.Join(", ", new List<T>(listToDebug).ConvertAll(i => i.ToString()).ToArray());
            
            if (context != null) 
                Debug.Log(str, context);
            
            else Debug.Log(str);
        }
        
        public static void Log<T>(T[] arrayToDebug, string message = "", Object context = null)
        {
            string str = message;
            if (arrayToDebug != null)
                str = str + string.Join(", ", new List<T>(arrayToDebug).ConvertAll(i => i.ToString()).ToArray());
            
            if (context != null) 
                Debug.Log(str, context);
            
            else Debug.Log(str);
        }

    }

}
