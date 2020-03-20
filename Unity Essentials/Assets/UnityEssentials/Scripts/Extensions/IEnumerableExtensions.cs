using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

static class IEnumerableExtensions
{
    /// <summary>
    /// Creates a new List with all the elements cloned in it.
    /// </summary>
    /// <returns>A new list with all elements of the original enumerable cloned in it.</returns>
    public static List<T> CloneAllToNewList<T>(this IEnumerable<T> enumerable) where T: ICloneable
    {
        return enumerable.Select(item => (T)item.Clone()).ToList();
    }
    
    /// <summary>
    /// Creates a new Array with all the elements cloned in it.
    /// </summary>
    /// <returns>A new array with all elements of the original enumerable cloned in it.</returns>
    public static T[] CloneAllToNewArray<T>(this IEnumerable<T> enumerable) where T: ICloneable
    {
        return enumerable.Select(item => (T)item.Clone()).ToArray();
    }

    /// <summary>
    /// Creates a 'Debug.Log' message with all the contents in the enumerable.
    /// </summary>
    /// <param name="message">The message that will be displayed at the beginning.</param>
    /// <param name="context"></param>
    /// <returns>Void</returns>
    public static void DebugLog<T>(this IEnumerable<T> enumerable, string separator = ", ", string message = "", Object context = null)
    {
        Debug.Log(message + enumerable.ToStringAllElements(separator), context);
    }
    
    /// <summary>
    /// Get an string of all elements.
    /// </summary>
    /// <param name="separator">The string that will be in-between each string of each element.</param>
    /// <returns>The result of all elements .ToString() concatenated separated by a separator (the default is ', ').</returns>
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
        return enumerable.GetShuffled(new Random());
    }

    /// <summary>
    /// Shuffles the enumerable using the Fisher-Yates-Durstenfeld method.
    /// </summary>
    /// <param name="rnd">Pseudo random number generator to be used.</param>
    /// <returns>A new enumerable with all the elements in the original shuffled.</returns>
    private static IEnumerable<T> GetShuffled<T>(this IEnumerable<T> source, Random rnd)
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
        return enumerable.GetRandomElement<T>(new Random());
    }

    /// <summary>
    /// Return a random element.
    /// </summary>
    /// <param name="rnd">Pseudo random number generator to be used.</param>
    /// <returns>A random element.</returns>
    public static T GetRandomElement<T>(this IEnumerable<T> enumerable, Random rnd)
    {
        int index = rnd.Next(0, enumerable.Count());
        return enumerable.ElementAt(index);
    }

}