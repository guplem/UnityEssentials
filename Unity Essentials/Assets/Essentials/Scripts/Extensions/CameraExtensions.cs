namespace UnityEngine
{
    /// <summary>
    /// Extensions for the Camera component
    /// </summary>
    
    public static class CameraExtensions
    {
        /// <summary>
        /// Linearly interpolates between two cameras.
        /// <para>When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.</para>
        /// </summary>
        public static void SetLerp(this Camera self, Camera a, Camera b, float t)
        {
            Color backgroundColor = Color.Lerp(a.backgroundColor,b.backgroundColor,t);
            float fieldOfView = Mathf.Lerp(a.fieldOfView, b.fieldOfView, t);
            float farClipPlane = Mathf.Lerp(a.farClipPlane, b.farClipPlane, t);
            float nearClipPlane = Mathf.Lerp(a.nearClipPlane, b.nearClipPlane, t);
            Rect rect = new Rect(Vector2.Lerp(a.rect.position, b.rect.position, t), Vector2.Lerp(a.rect.size, b.rect.size, t));
            //Rect rect = self.rect;
            float depth = Mathf.Lerp(a.depth, b.depth, t);
            
            self.SetProperties(backgroundColor, fieldOfView, farClipPlane, nearClipPlane, rect, depth);
        }

        /// <summary>
        /// Sets the camera parameters that can be linearly interpolated.
        /// </summary>
        /// <param name="backgroundColor">The color with which the screen will be cleared.</param>
        /// <param name="fieldOfView">The field of view of the camera in degrees.</param>
        /// <param name="farClipPlane">The distance of the far clipping plane from the Camera, in world units.</param>
        /// <param name="nearClipPlane">The distance of the near clipping plane from the the Camera, in world units.</param>
        /// <param name="rect">Where on the screen is the camera rendered in normalized coordinates.</param>
        /// <param name="depth">Camera's depth in the camera rendering order.</param>
        public static void SetProperties(this Camera cam, Color backgroundColor, float fieldOfView, float farClipPlane, float nearClipPlane, Rect rect, float depth)
        {
            cam.backgroundColor = backgroundColor;
            cam.fieldOfView = fieldOfView;
            cam.farClipPlane = farClipPlane;
            cam.nearClipPlane = nearClipPlane;
            cam.rect = rect;
            cam.depth = depth;
        }
    }
}
 
