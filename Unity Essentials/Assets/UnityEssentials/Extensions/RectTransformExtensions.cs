using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    // Setters
    
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
 
    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
 
    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
 
    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
    
    // Getters

    public static float GetHeightAnchors(this RectTransform rt)
    {
        return rt.sizeDelta.y;
    }
    
    public static float GetWidthAnchors(this RectTransform rt)
    {
        return rt.sizeDelta.x;
    }
    
    public static float GetHeightTransform(this RectTransform rt)
    {
        return rt.rect.y;
    }
    
    public static float GetWidthTransform(this RectTransform rt)
    {
        return rt.rect.x;
    }
    
    // Top/Bottom/Right/Left
    public static float GetLeft(this RectTransform rt)
    {
        return rt.offsetMin.x; //return rt.rect.xMin;
    }
 
    public static float GetRight(this RectTransform rt)
    {
        return -rt.offsetMax.x; //return rt.rect.xMax;
    }
 
    public static float GetTop(this RectTransform rt)
    {
        return -rt.offsetMax.y; //return rt.rect.yMin;
    }
 
    public static float GetBottom(this RectTransform rt)
    {
        return rt.offsetMin.y; //return rt.rect.yMax;
    }

}
