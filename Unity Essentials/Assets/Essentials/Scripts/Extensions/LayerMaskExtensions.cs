namespace UnityEngine
{
    /// <summary>
    /// Extensions for LayerMask
    /// </summary>
    
    public static class LayerMaskExtensions
    {
    
        /// <summary>
        /// Checks if the LayerMask contains a given layer.
        /// </summary>
        /// <param name="layerNumber">The number of the layer to check if is in the LayerMask</param>
        /// <returns>True if the LayerMask contains the given layer number. False if it does not.</returns>

        public static bool Contains(this LayerMask mask, int layerNumber)
        {
            return mask == (mask | (1 << layerNumber));
        }
    }
}
