namespace UnityEngine
{
    /// <summary>
    /// Extensions for string
    /// </summary>
    
    public static class StringExtensions
    {

        /// <summary>
        /// Ensures that the string ends with a dot ('.').
        /// </summary>
        /// <returns>The string with an added dot at the end if it was not there.</returns>
        public static string EnsureEndsWithDot(this string str)
        {
            if (!str.EndsWith(".")) return str + ".";
            return str;
        }
    
        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <returns>True if the value parameter is null or Empty, or if value consists exclusively of white-space characters.</returns>
        public static bool IsNullEmptyOrWhiteSpace(this string str)
        {
            return /*string.IsNullOrEmpty(str) ||*/ string.IsNullOrWhiteSpace(str); // 'IsNullOrWhiteSpace' already checks for emptiness
        }
    
        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("").
        /// </summary>
        /// <returns>True if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    
    }
}
