using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions
{

    public static string EnsureEndsWithDot(this string str)
    {
        if (!str.EndsWith(".")) return str + ".";
        return str;
    }
    
    public static bool IsNullEmptyOrWhiteSpace(this string str)
    {
        return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
    }
    
}
