using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
    /// <summary>
    /// Extensions for IEnumerable
    /// </summary>
    
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Creates a new enumerable with all the elements of the original one cloned in it.
        /// </summary>
        /// <returns>A new IEnumerable with all elements of the original enumerable cloned in it.</returns>
        public static IEnumerable<T> CloneAll<T>(this IEnumerable<T> enumerable) where T: ICloneable
        {
            return enumerable.Select(item => (T) item.Clone());
        }

        /// <summary>
        /// Creates a 'Debug.Log' message with all the contents in the enumerable.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <param name="message">The message that will be displayed at the beginning of the 'Debug.Log' message.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void DebugLog<T>(this IEnumerable<T> enumerable, string separator = ", ", string message = "", Object context = null)
        {
            Debug.Log(message + enumerable.ToStringAllElements(separator), context);
        }
        
        /// <summary>
        /// Creates a 'Debug.LogWarning' message with all the contents in the enumerable.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <param name="message">The message that will be displayed at the beginning of the 'Debug.Log' message.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void DebugLogWarning<T>(this IEnumerable<T> enumerable, string separator = ", ", string message = "", Object context = null)
        {
            Debug.LogWarning(message + enumerable.ToStringAllElements(separator), context);
        }
        
        /// <summary>
        /// Creates a 'Debug.LogError' message with all the contents in the enumerable.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <param name="message">The message that will be displayed at the beginning of the 'Debug.Log' message.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void DebugLogError<T>(this IEnumerable<T> enumerable, string separator = ", ", string message = "", Object context = null)
        {
            Debug.LogError(message + enumerable.ToStringAllElements(separator), context);
        }
    
        /// <summary>
        /// Get an string of all elements.
        /// </summary>
        /// <param name="separator">The string that will be in-between each string of each element (the default is ', ').</param>
        /// <returns>The result of all elements .ToString() concatenated separated by a separator string.</returns>
        public static string ToStringAllElements<T>(this IEnumerable<T> enumerable, string separator = ", ")
        {
            return string.Join(separator, new List<T>(enumerable));
        }

        /// <summary>
        /// Shuffles the enumerable using the Fisher-Yates-Durstenfeld method.
        /// </summary>
        /// <returns>A new enumerable with all the elements in the original shuffled.</returns>
        public static IEnumerable<T> GetShuffled<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.GetShuffled(new System.Random());
        }

        /// <summary>
        /// Shuffles the enumerable using the Fisher-Yates-Durstenfeld method.
        /// </summary>
        /// <param name="rnd">Pseudo random number generator to be used.</param>
        /// <returns>A new enumerable with all the elements in the original shuffled.</returns>
        private static IEnumerable<T> GetShuffled<T>(this IEnumerable<T> source, System.Random rnd)
        {
            List<T> buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rnd.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        /// <summary>
        /// Return a random element.
        /// </summary>
        /// <returns>A random element.</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.GetRandomElement<T>(new System.Random());
        }

        /// <summary>
        /// Return a random element.
        /// </summary>
        /// <param name="rnd">Pseudo random number generator to be used.</param>
        /// <returns>A random element.</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> enumerable, System.Random rnd)
        {
            int index = rnd.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    
        /// <summary>
        /// Copies the elements to a new HashSet.
        /// </summary>
        /// <param name="comparer">The IEqualityComparer<T> implementation to use when comparing values in the set, or null to use the default EqualityComparer<T> implementation for the set type.</param>
        /// <returns>A new HashSet with all the elements in the collection.</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }
        
        /// <summary>
        /// Indicates whether the IEnumerable is null or does not contain any element.
        /// </summary>
        /// <returns>True if the value IEnumerable is null or does not contain any element. Otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

    }
}