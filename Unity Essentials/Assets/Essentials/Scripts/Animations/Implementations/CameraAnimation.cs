using System;

namespace UnityEngine
{
    /// <summary>
    /// Allows the animation of Camera components.
    /// </summary>
    [Serializable]
    public class CameraAnimation : SimpleAnimation
    {
        /// <summary>
        /// The Camera to animate
        /// </summary>
        [Tooltip("The Camera to animate")]
        [SerializeField] public Camera cameraToAnimate;
        /// <summary>
        /// The state of the Camera at the start of the animation
        /// </summary>
        [Tooltip("The state of the Camera at the start of the animation")]
        [SerializeField] public Camera originCamera;
        /// <summary>
        /// The state of the Camera at the end of the animation
        /// </summary>
        [Tooltip("The state of the Camera at the end of the animation")]
        [SerializeField] public Camera destinationCamera;

        
        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public CameraAnimation() : this(null, null, null) { } 
        
        public CameraAnimation(Camera cameraToAnimate, Camera destination, Camera origin, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.cameraToAnimate = cameraToAnimate;
            this.originCamera = origin;
            this.destinationCamera = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        
        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            cameraToAnimate.SetLerp(originCamera, destinationCamera, currentAnimationCurveValue);

            return endOfAnimation;
        }
        

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            return cameraToAnimate;
        }
        
    }
}
