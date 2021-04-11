using System;

namespace UnityEngine
{
    [Serializable]
    public class FloatAnimation : SimpleAnimation
    {
        [NonSerialized] public float floatToAnimate;
        [SerializeField] public float originFloat;
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
            
            //floatToAnimate = Color.Lerp(originFloat, destinationFloat, currentAnimationValue);
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
