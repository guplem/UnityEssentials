namespace UnityEngine
{
    /// <summary>
    /// Extensions for Component
    /// </summary>
    /// 
    public static class ComponentExtensions
    {
        /// <summary>
        /// Get a component. Log an error if it is not found.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <returns>The component, if found.</returns>
        public static T GetComponentRequired<T>(this Component self) where T : Component
        {
            T component = self.GetComponent<T>();

            if (component == null) Debug.LogError("Could not find " + typeof(T) + " on " + self.name);

            return component;
        }
    }
}

