using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class ListExtensions
{
    public static List<T> Clone<T>(this IEnumerable<T> listToClone) where T: ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static void DebugLog<T>(this IEnumerable<T> listToDebug, string message = "")
    {
        Debug.Log(message + string.Join(", ",
                      new List<T>(listToDebug)
                          .ConvertAll(i => i.ToString())
                          .ToArray()));
    }
    
}
