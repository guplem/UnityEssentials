using System;

namespace UnityEngine
{
    [Serializable]
    public class CameraAnimation : SimpleAnimation
    {
        [SerializeField] public Camera cameraToAnimate;
        [SerializeField] public Camera originCamera;
        [SerializeField] public Camera destinationCamera;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public CameraAnimation() : this(null, null, null) { } 
        
        public CameraAnimation(Camera cameraToAnimate, Camera destination, Camera origin, float duration = 1f, Curve curve = Curve.EaseInOut)
        {
            this.cameraToAnimate = cameraToAnimate;
            this.originCamera = origin;
            this.destinationCamera = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            cameraToAnimate.SetLerp(originCamera, destinationCamera, currentAnimationValue);

            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            return cameraToAnimate;
        }
    }
}
