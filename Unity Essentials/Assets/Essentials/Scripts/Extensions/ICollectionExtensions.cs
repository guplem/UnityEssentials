﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public static class ICollectionExtensions
{
    /// <summary>
    /// Removes the first element of the collection.
    /// </summary>
    /// <returns>Returns the removed element, or a default value if no element is found.</returns>
    public static T RemoveFirst<T>(this ICollection<T> collection)
    {
        T element = collection.FirstOrDefault();
        if (element != null)
            collection.Remove(element);
        return element;
    }
}
