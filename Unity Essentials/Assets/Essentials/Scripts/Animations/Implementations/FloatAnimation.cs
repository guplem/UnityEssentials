using System;

namespace UnityEngine
{
    /// <summary>
    /// Allows the animation of a float.
    /// </summary>
    [Serializable]
    public class FloatAnimation : SimpleAnimation
    {
        /// <summary>
        /// The animated float
        /// </summary>
        [Tooltip("The animated float")]
        [NonSerialized] public float floatToAnimate;
        /// <summary>
        /// The float at the start of the animation
        /// </summary>
        [Tooltip("The float at the start of the animation")]
        [SerializeField] public float originFloat;
        /// <summary>
        /// The float at the end of the animation
        /// </summary>
        [Tooltip("The float at the end of the animation")]
        [SerializeField] public float destinationFloat;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public FloatAnimation() : this(0f, 0f, 0f) { } 
        
        public FloatAnimation(float floatToAnimate, float destination, float origin, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.floatToAnimate = floatToAnimate;
            this.originFloat = origin;
            this.destinationFloat = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            floatToAnimate = Mathf.Lerp(originFloat, destinationFloat, currentAnimationCurveValue);
            
            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            if (displayWarningIfNotApplicable)
                Debug.LogWarning("Trying to obtain the animated object from a FloatAnimation. This action is not supported. Access the 'floatToAnimate' instead.");
            return null;
        }
    }
}
