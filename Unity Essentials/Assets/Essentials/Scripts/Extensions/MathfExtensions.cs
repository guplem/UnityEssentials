using System;

namespace UnityEngine
{
    
    /// <summary>
    /// Extensions for the Mathf class
    /// </summary>
    
    public static class MathfExtensions
    {
        /// <summary>
        /// Calculates if the float is multiple of another float.
        /// </summary>
        /// <param name="n">The number that we want to know if it is multiple of.</param>
        /// <param name="tolerance">The tolerance that we want to set to deal with potential mathematical errors.</param>
        /// <returns></returns>
        public static bool IsMultipleOf(this float v, float n, float tolerance = 0.001f)
        {
            return Math.Abs(v % n) < tolerance;
        }
        /// <summary>
        /// Calculates if the float is multiple of an int.
        /// </summary>
        /// <param name="n">The number that we want to know if it is multiple of.</param>
        /// <param name="tolerance">The tolerance that we want to set to deal with potential mathematical errors.</param>
        /// <returns></returns>
        public static bool IsMultipleOf(this float v, int n, float tolerance = 0.001f)
        {
            return Math.Abs(Math.Abs(v % n)) < tolerance;
        }
        /// <summary>
        /// Calculates if the int is multiple of another int.
        /// </summary>
        /// <param name="n">The number that we want to know if it is multiple of.</param>
        /// <returns></returns>
        public static bool IsMultipleOf(this int v, int n)
        {
            return v % n == 0;
        }
        /// <summary>
        /// Calculates if the int is multiple of a float.
        /// </summary>
        /// <param name="n">The number that we want to know if it is multiple of.</param>
        /// <param name="tolerance">The tolerance that we want to set to deal with potential mathematical errors.</param>
        /// <returns></returns>
        public static bool IsMultipleOf(this int v, float n, float tolerance = 0.001f)
        {
            return Math.Abs(Math.Abs(v % n)) < tolerance;
        }
    }
}
