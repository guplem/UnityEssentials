using System;

namespace UnityEngine
{
    /// <summary>
    /// Allows the animation of Transform components.
    /// </summary>
    [Serializable]
    public class TransformAnimation : SimpleAnimation
    {
        /// <summary>
        /// The Transform to animate
        /// </summary>
        [Tooltip("The Transform to animate")]
        [SerializeField] public Transform transformToAnimate;
        /// <summary>
        /// The state of the Transform at the start of the animation
        /// </summary>
        [Tooltip("The state of the Transform at the start of the animation")]
        [SerializeField] public Transform originTransform;
        /// <summary>
        /// The state of the Transform at the end of the animation
        /// </summary>
        [Tooltip("The state of the Transform at the end of the animation")]
        [SerializeField] public Transform destinationTransform;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public TransformAnimation() : this(null, null, null) { } 
        
        public TransformAnimation(Transform transformToAnimate, Transform destination, Transform origin, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.transformToAnimate = transformToAnimate;
            this.originTransform = origin;
            this.destinationTransform = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            transformToAnimate.SetLerp(originTransform.transform, destinationTransform, curve.Evaluate(timeStamp / duration));

            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            return transformToAnimate;
        }
    }
}