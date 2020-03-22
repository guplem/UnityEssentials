using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
    /// <summary>
    /// Loops the integer between a minimum and maximum.
    /// </summary>
    /// <param name="exclusiveMaximum">The exclusive maximum value than can be obtained.</param>
    /// <param name="inclusiveMinimum">The minimum value that can be obtained.</param>
    /// <param name="variancePerStep">The value added to the int every time that the method is called.</param>
    /// <returns>Returns a the result of adding the 'variancePerStep' (default to 1) to the original integer. The value is always between the minimum (inclusive, default to 0) and the maximum (exclusive).</returns>
    public static int GetLooped(this int intToBeLooped, int exclusiveMaximum, int inclusiveMinimum = 0, int variancePerStep = 1)
    {
        int returnInt = intToBeLooped + variancePerStep;
        if (returnInt >= exclusiveMaximum)
            returnInt = inclusiveMinimum;
        return returnInt;
    }


}
