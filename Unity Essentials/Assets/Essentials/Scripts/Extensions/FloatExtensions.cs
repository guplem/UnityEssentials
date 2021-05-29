namespace UnityEngine
{
    /// <summary>
    /// Extensions for float
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Loops the float between a minimum and maximum.
        /// </summary>
        /// <param name="exclusiveMaximum">The exclusive maximum value than can be obtained.</param>
        /// <param name="inclusiveMinimum">The minimum value that can be obtained.</param>
        /// <param name="variancePerStep">The value added to the int every time that the method is called.</param>
        /// <returns>Returns a the result of adding the 'variancePerStep' (default to 1f) to the original integer. The value is always between the minimum (inclusive, default to 0f) and the maximum (exclusive).</returns>
        public static float Loop(this float floatToBeLooped, float exclusiveMaximum, float inclusiveMinimum = 0f, float variancePerStep = 1f)
        {
            float returnInt = floatToBeLooped + variancePerStep;
            if (returnInt >= exclusiveMaximum)
                returnInt = inclusiveMinimum;
            return returnInt;
        }

        /// <summary>
        /// Maps the value from one range to another range.
        /// </summary>
        /// <param name="originalRangeMin">The minimum value in the original range.</param>
        /// <param name="originalRangeMax">The maximum value in the original range.</param>
        /// <param name="newRangeMin">The minimum value in the new range.</param>
        /// <param name="newRangeMax">The maximum value in the new range.</param>
        /// <returns>The value mapped to the new range from the original one.</returns>
        public static float Map(this float value, float originalRangeMin, float originalRangeMax, float newRangeMin = 0f, float newRangeMax = 1f)
        {
            return ((value - originalRangeMin) / (originalRangeMax - originalRangeMin) * (newRangeMax - newRangeMin)) + newRangeMin;
        }
    }
}
