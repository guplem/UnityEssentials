namespace UnityEngine
{
    /// <summary>
    /// Extensions for GameObject
    /// </summary>
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

        public static void SetLayerRecursively(this GameObject self, int newLayer)
        {
            if (null == self)
            {
                return;
            }

            self.layer = newLayer;

            foreach (Transform child in self.transform)
            {
                if (null == child)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}