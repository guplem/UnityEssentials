using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    /// <summary>
    /// Sets the distance from the anchor at the left of the rect transform left edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the left.</param>
    public static void SetLeft(this RectTransform rt, float distance)
    {
        rt.offsetMin = new Vector2(distance, rt.offsetMin.y);
    }
    /// <summary>
    /// Sets the distance from the anchor at the right of the rect transform right edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the right.</param>
    public static void SetRight(this RectTransform rt, float distance)
    {
        rt.offsetMax = new Vector2(-distance, rt.offsetMax.y);
    }
    /// <summary>
    /// Sets the distance from the anchor at the top of the rect transform top edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the top.</param>
    public static void SetTop(this RectTransform rt, float distance)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -distance);
    }
    /// <summary>
    /// Sets the distance from the anchor at the bottom of the rect transform bottom edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the bottom.</param>
    public static void SetBottom(this RectTransform rt, float distance)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, distance);
    }
    
    /// <summary>
    /// Gets the distance from the anchor at the left of the rect transform left edge.
    /// </summary>
    /// <returns>The distance from the anchor at the left of the rect transform left edge.</returns>
    public static float GetLeft(this RectTransform rt)
    {
        return rt.offsetMin.x;
    }
    /// <summary>
    /// Gets the distance from the anchor at the right of the rect transform right edge.
    /// </summary>
    /// <returns>The distance from the anchor at the right of the rect transform right edge.</returns>
    public static float GetRight(this RectTransform rt)
    {
        return -rt.offsetMax.x;
    }
    /// <summary>
    /// Gets the distance from the anchor at the top of the rect transform top edge.
    /// </summary>
    /// <returns>The distance from the anchor at the top of the rect transform top edge.</returns>
    public static float GetTop(this RectTransform rt)
    {
        return -rt.offsetMax.y;
    }
    /// <summary>
    /// Gets the distance from the anchor at the bottom of the rect transform bottom edge.
    /// </summary>
    /// <returns>The distance from the anchor at the bottom of the rect transform bottom edge.</returns>
    public static float GetBottom(this RectTransform rt)
    {
        return rt.offsetMin.y;
    }
    
}
