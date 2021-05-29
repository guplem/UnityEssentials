namespace UnityEngine
{
    /// <summary>
    /// Class containing a minimum and max Vectors with its components between 0 and 1
    /// </summary>
    public struct MinMax01
    {
        public Vector2 min { get; private set; }
        public Vector2 max { get; private set; }

        public MinMax01(Vector2 min, Vector2 max)
        {
            this.min = new Vector2(Mathf.Clamp01(min.x), Mathf.Clamp01(min.y));
            this.max = new Vector2(Mathf.Clamp01(max.x), Mathf.Clamp01(max.y));
        }

        public MinMax01(float minx, float miny, float maxx, float maxy)
        {
            this.min = new Vector2(Mathf.Clamp01(minx), Mathf.Clamp01(miny));
            this.max = new Vector2(Mathf.Clamp01(maxx), Mathf.Clamp01(maxy));
        }
    }
}