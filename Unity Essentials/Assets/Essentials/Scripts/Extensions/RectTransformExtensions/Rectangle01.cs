namespace UnityEngine
{
    public struct Rectangle01
    {
        public Vector2 min;
        public Vector2 max;

        public Rectangle01(Vector2 min, Vector2 max)
        {
            this.min = new Vector2(Mathf.Clamp01(min.x), Mathf.Clamp01(min.y));
            this.max = new Vector2(Mathf.Clamp01(max.x), Mathf.Clamp01(max.y));
        }

        public Rectangle01(float minx, float miny, float maxx, float maxy)
        {
            this.min = new Vector2(Mathf.Clamp01(minx), Mathf.Clamp01(miny));
            this.max = new Vector2(Mathf.Clamp01(maxx), Mathf.Clamp01(maxy));
        }
    }
}