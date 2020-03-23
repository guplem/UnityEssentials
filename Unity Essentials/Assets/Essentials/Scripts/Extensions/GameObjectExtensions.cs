using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Get a component. Log an error if it is not found.
    /// </summary>
    /// <typeparam name="T">The type of component to get.</typeparam>
    /// <returns>The component, if found.</returns>
    public static T GetComponentRequired<T>(this GameObject self) where T : Component
    {
        T component = self.GetComponent<T>();

        if (component == null) Debug.LogError("Could not find " + typeof(T) + " on " + self.name);

        return component;
    }
    
    /// <summary>
    /// Destroys immediately the children of a transform.
    /// <para>This method is not recommended to use. Instead use 'DestroyAllChildren'."</para>
    /// </summary>
    /// <param name="exceptions">Transforms that must not be destroyed.</param>
    public static void DestroyImmediateAllChildren(this GameObject self, IEnumerable<Transform> exceptions = null)
    {
        self.transform.DestroyImmediateAllChildren(exceptions);
    }
    
    /// <summary>
    /// Destroys immediately the children of a transform.
    /// </summary>
    /// <param name="exceptions">Transforms that must not be destroyed.</param>
    public static void DestroyAllChildren(this GameObject self, IEnumerable<Transform> exceptions = null)
    {
        self.transform.DestroyAllChildren(exceptions);
    }
    
}