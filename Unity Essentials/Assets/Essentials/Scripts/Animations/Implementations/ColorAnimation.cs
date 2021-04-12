using System;

namespace UnityEngine
{
    [Serializable]
    public class ColorAnimation : SimpleAnimation
    {
        [NonSerialized] public Color colorToAnimate;
        [SerializeField] public Color originColor;
        [SerializeField] public Color destinationColor;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public ColorAnimation() : this(Color.white, Color.white, Color.white) { } 
        
        public ColorAnimation(Color colorToAnimate, Color destination, Color origin, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.colorToAnimate = colorToAnimate;
            this.originColor = origin;
            this.destinationColor = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            colorToAnimate = Color.Lerp(originColor, destinationColor, currentAnimationCurveValue);

            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            if (displayWarningIfNotApplicable)
                Debug.LogWarning("Trying to obtain the animated object from a ColorAnimation. This action is not supported. Access the 'colorToAnimate' instead.");
            return null;
        }
    }
}
