using System.Collections.Generic;

namespace UnityEngine
{
    /// <summary>
    /// Class containing additional methods to ease debugging while developing a game.
    /// </summary>
    public class DebugEssentials : Debug
    {
        
        /// <summary>
        /// Creates a 'Debug.Log' message with all the contents in the enumerable.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <param name="message">The message that will be displayed at the beginning of the 'Debug.Log' message.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogEnumerable<T>(IEnumerable<T> enumerableToDebug, string separator = ", ", string message = "", Object context = null)
        {
            enumerableToDebug.DebugLog(separator, message, context);
        }
        
        /// <summary>
        /// Creates a 'Debug.LogWarning' message with all the contents in the enumerable.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <param name="message">The message that will be displayed at the beginning of the 'Debug.Log' message.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogWarningEnumerable<T>(IEnumerable<T> enumerableToDebug, string separator = ", ", string message = "", Object context = null)
        {
            enumerableToDebug.DebugLogWarning(separator, message, context);
        }
        
        /// <summary>
        /// Creates a 'Debug.LogError' message with all the contents in the enumerable.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <param name="message">The message that will be displayed at the beginning of the 'Debug.Log' message.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogErrorEnumerable<T>(IEnumerable<T> enumerableToDebug, string separator = ", ", string message = "", Object context = null)
        {
            enumerableToDebug.DebugLogError(separator, message, context);
        }
    }

}
