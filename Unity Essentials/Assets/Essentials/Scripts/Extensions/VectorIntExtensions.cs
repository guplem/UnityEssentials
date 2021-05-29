namespace UnityEngine
{
    /// <summary>
    /// Extensions for VectorInt
    /// </summary>
    public static class VectorIntExtensions
    {
        /// <summary>
        /// Creates a new vector with the same values as the original.
        /// </summary>
        /// <returns>A new vector with the same values as the original.</returns>
        public static Vector2Int Clone(this Vector2Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'y' parameter but a new one for the 'x'.
        /// </summary>
        /// <param name="x">The desired value for the 'x' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'y' parameter but a new one for the 'x'.</returns>
        public static Vector2Int WithX(this Vector2Int v, int x = 0)
        {
            return new Vector2Int(x, v.y);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'x' parameter but a new one for the 'y'.
        /// </summary>
        /// <param name="y">The desired value for the 'y' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'x' parameter but a new one for the 'y'.</returns>
        public static Vector2Int WithY(this Vector2Int v, int y = 0)
        {
            return new Vector2Int(v.x, y);
        }

        /// <summary>
        /// Creates a new Vector3Int keeping the values from the 'x' and 'y' parameters of the original Vector2Int in that order.
        /// </summary>
        /// <param name="x">The desired value for the 'x' component on the new Vector 3.</param>
        /// <returns>A new Vector3Int with the 'x' and 'y' values equal to the original Vector2Int.</returns>
        public static Vector3Int ToVector3IntNewX(this Vector2Int v, int x = 0)
        {
            return new Vector3Int(x, v.x, v.y);
        }

        /// <summary>
        /// Creates a new Vector3Int keeping the values from the 'x' and 'y' parameters of the original Vector2Int in that order.
        /// </summary>
        /// <param name="y">The desired value for the 'y' component on the new Vector 3.</param>
        /// <returns>A new Vector3Int with the 'x' and 'y' values equal to the original Vector2Int.</returns>
        public static Vector3Int ToVector3IntNewY(this Vector2Int v, int y = 0)
        {
            return new Vector3Int(v.x, y, v.y);
        }

        /// <summary>
        /// Creates a new Vector3Int keeping the values from the 'x' and 'y' parameters of the original Vector2Int in that order.
        /// </summary>
        /// <param name="z">The desired value for the 'z' component on the new Vector 3.</param>
        /// <returns>A new Vector3Int with the 'x' and 'y' values equal to the original Vector2Int.</returns>
        public static Vector3Int ToVector3IntNewZ(this Vector2Int v, int z = 0)
        {
            return new Vector3Int(v.x, v.y, z);
        }

        /// <summary>
        /// Creates a new vector with the same values as the original.
        /// </summary>
        /// <returns>A new vector with the same values as the original.</returns>
        public static Vector3Int Clone(this Vector3Int v)
        {
            return new Vector3Int(v.x, v.y, v.z);
        }

        /// <summary>
        /// Creates a new Vector2Int keeping the values from the 'y' and 'z' parameters of the original Vector3Int.
        /// </summary>
        /// <returns>A new Vector2Int with the 'y' and 'z' values equal to the original Vector3Int.</returns>
        public static Vector2Int ToVector2IntWithoutX(this Vector3Int v)
        {
            return new Vector2Int(v.y, v.z);
        }

        /// <summary>
        /// Creates a new Vector2Int keeping the values from the 'x' and 'z' parameters of the original Vector3Int.
        /// </summary>
        /// <returns>A new Vector2Int with the 'x' and 'z' values equal to the original Vector3Int.</returns>
        public static Vector2Int ToVector2IntWithoutY(this Vector3Int v)
        {
            return new Vector2Int(v.x, v.z);
        }

        /// <summary>
        /// Creates a new Vector2Int keeping the values from the 'x' and 'y' parameters of the original Vector3Int.
        /// </summary>
        /// <returns>A new Vector2Int with the 'x' and 'y' values equal to the original Vector3Int.</returns>
        public static Vector2Int ToVector2IntWithoutZ(this Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'y' and 'z' parameter but a new one for the 'x'.
        /// </summary>
        /// <param name="x">The desired value for the 'x' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'y' and 'z' parameter but a new one for the 'x'.</returns>
        public static Vector3Int WithX(this Vector3Int v, int x = 0)
        {
            return new Vector3Int(x, v.y, v.z);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'x' and 'z' parameter but a new one for the 'y'.
        /// </summary>
        /// <param name="y">The desired value for the 'y' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'x' and 'z' parameter but a new one for the 'y'.</returns>
        public static Vector3Int WithY(this Vector3Int v, int y = 0)
        {
            return new Vector3Int(v.x, y, v.z);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'x' and 'y' parameter but a new one for the 'z'.
        /// </summary>
        /// <param name="z">The desired value for the 'z' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'x' and 'y' parameter but a new one for the 'z'.</returns>
        public static Vector3Int WithZ(this Vector3Int v, int z = 0)
        {
            return new Vector3Int(v.x, v.y, z);
        }

        /// <summary>
        /// Creates a new Vector3 with the values in the original vector.
        /// </summary>
        /// <returns>A new Vector3 with the values in the original vector.</returns>
        public static Vector3 ToVectorFloat(this Vector3Int v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        /// <summary>
        /// Creates a new Vector2 with the values in the original vector.
        /// </summary>
        /// <returns>A new Vector2 with the values in the original vector.</returns>
        public static Vector2 ToVectorFloat(this Vector2Int v)
        {
            return new Vector2(v.x, v.y);
        }
    }
}