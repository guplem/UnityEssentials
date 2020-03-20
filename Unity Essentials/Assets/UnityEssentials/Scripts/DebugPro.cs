using System.Collections.Generic;

namespace UnityEngine
{
    public class DebugPro : Debug
    {
        public static void LogEnumerable<T>(IEnumerable<T> enumerableToDebug, string separator = ", ", string message = "", Object context = null)
        {
            enumerableToDebug.DebugLog(separator, message, context);
        }
    }

}
