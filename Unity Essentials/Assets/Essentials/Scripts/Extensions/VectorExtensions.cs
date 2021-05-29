namespace UnityEngine
{
    /// <summary>
    /// Extensions for Vector
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Creates a new vector with the same values as the original.
        /// </summary>
        /// <returns>A new vector with the same values as the original.</returns>
        public static Vector2 Clone(this Vector2 v)
        {
            return new Vector2(v.x, v.y);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'y' parameter but a new one for the 'x'.
        /// </summary>
        /// <param name="x">The desired value for the 'x' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'y' parameter but a new one for the 'x'.</returns>
        public static Vector2 WithX(this Vector2 v, float x = 0f)
        {
            return new Vector2(x, v.y);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'x' parameter but a new one for the 'y'.
        /// </summary>
        /// <param name="y">The desired value for the 'y' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'x' parameter but a new one for the 'y'.</returns>
        public static Vector2 WithY(this Vector2 v, float y = 0f)
        {
            return new Vector2(v.x, y);
        }

        /// <summary>
        /// Creates a new Vector3 keeping the values from the 'x' and 'y' parameters of the original Vector2 in that order.
        /// </summary>
        /// <param name="x">The desired value for the 'x' component on the new Vector 3.</param>
        /// <returns>A new Vector3 with the 'x' and 'y' values equal to the original Vector2.</returns>
        public static Vector3 ToVector3NewX(this Vector2 v, float x = 0f)
        {
            return new Vector3(x, v.x, v.y);
        }

        /// <summary>
        /// Creates a new Vector3 keeping the values from the 'x' and 'y' parameters of the original Vector2 in that order.
        /// </summary>
        /// <param name="y">The desired value for the 'y' component on the new Vector 3.</param>
        /// <returns>A new Vector3 with the 'x' and 'y' values equal to the original Vector2.</returns>
        public static Vector3 ToVector3NewY(this Vector2 v, float y = 0f)
        {
            return new Vector3(v.x, y, v.y);
        }

        /// <summary>
        /// Creates a new Vector3 keeping the values from the 'x' and 'y' parameters of the original Vector2 in that order.
        /// </summary>
        /// <param name="z">The desired value for the 'z' component on the new Vector 3.</param>
        /// <returns>A new Vector3 with the 'x' and 'y' values equal to the original Vector2.</returns>
        public static Vector3 ToVector3NewZ(this Vector2 v, float z = 0f)
        {
            return new Vector3(v.x, v.y, z);
        }

        /// <summary>
        /// Creates a new vector with the same values as the original.
        /// </summary>
        /// <returns>A new vector with the same values as the original.</returns>
        public static Vector3 Clone(this Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        /// <summary>
        /// Creates a new Vector2 keeping the values from the 'y' and 'z' parameters of the original Vector3.
        /// </summary>
        /// <returns>A new Vector2 with the 'y' and 'z' values equal to the original Vector3.</returns>
        public static Vector2 ToVector2WithoutX(this Vector3 v)
        {
            return new Vector2(v.y, v.z);
        }

        /// <summary>
        /// Creates a new Vector2 keeping the values from the 'x' and 'z' parameters of the original Vector3.
        /// </summary>
        /// <returns>A new Vector2 with the 'x' and 'z' values equal to the original Vector3.</returns>
        public static Vector2 ToVector2WithoutY(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        /// <summary>
        /// Creates a new Vector2 keeping the values from the 'x' and 'y' parameters of the original Vector3.
        /// </summary>
        /// <returns>A new Vector2 with the 'x' and 'y' values equal to the original Vector3.</returns>
        public static Vector2 ToVector2WithoutZ(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'y' and 'z' parameter but a new one for the 'x'.
        /// </summary>
        /// <param name="x">The desired value for the 'x' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'y' and 'z' parameter but a new one for the 'x'.</returns>
        public static Vector3 WithX(this Vector3 v, float x = 0f)
        {
            return new Vector3(x, v.y, v.z);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'x' and 'z' parameter but a new one for the 'y'.
        /// </summary>
        /// <param name="y">The desired value for the 'y' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'x' and 'z' parameter but a new one for the 'y'.</returns>
        public static Vector3 WithY(this Vector3 v, float y = 0f)
        {
            return new Vector3(v.x, y, v.z);
        }

        /// <summary>
        /// Creates a new vector with the same value for the 'x' and 'y' parameter but a new one for the 'z'.
        /// </summary>
        /// <param name="z">The desired value for the 'z' component on the new vector.</param>
        /// <returns>A new vector with the same value for the 'x' and 'y' parameter but a new one for the 'z'.</returns>
        public static Vector3 WithZ(this Vector3 v, float z = 0f)
        {
            return new Vector3(v.x, v.y, z);
        }

        /// <summary>
        /// Calculates the nearest position in a given line or segment.
        /// </summary>
        /// <param name="origin">The origin point of the segment.</param>
        /// <param name="end">The end point of the segment.</param>
        /// <param name="clampInsideLineLength">If the result must be inside the given segment. If false, the origin and end points will be used to calculate the direction of an infinite line and the result will be within it.</param>
        /// <returns>The closest point to the segment or the line depending on the value of the 'clampInsideLineLength parameter.</returns>
        public static Vector3 NearestPointOnLine(this Vector3 point, Vector3 origin, Vector3 end,
            bool clampInsideLineLength = true)
        {
            //Get heading
            Vector3 direction = (end - origin);
            direction.Normalize();

            Vector3 lhs = point - origin;
            float dotP = Vector3.Dot(lhs, direction);

            if (clampInsideLineLength)
            {
                float magnitudeMax = direction.magnitude;
                dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            }

            return origin + direction * dotP;
        }

        /// <summary>
        /// Creates a new Vector3Int with the values in the original vector.
        /// </summary>
        /// <param name="round">If the values should be rounded to the nearest integer.</param>
        /// <returns>A new Vector3Int with the values in the original vector.</returns>
        public static Vector3Int ToVectorInt(this Vector3 v, bool round = false)
        {
            if (round)
                return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
            return new Vector3Int((int) v.x, (int) v.y, (int) v.z);
        }

        /// <summary>
        /// Creates a new Vector2Int with the values in the original vector.
        /// </summary>
        /// <param name="round">If the values should be rounded to the nearest integer.</param>
        /// <returns>A new Vector3Int with the values in the original vector.</returns>
        public static Vector2Int ToVectorInt(this Vector2 v, bool round = false)
        {
            if (round)
                return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
            return new Vector2Int((int) v.x, (int) v.y);
        }
        

    }
}