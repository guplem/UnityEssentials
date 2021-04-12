using System;

namespace UnityEngine
{
    [Serializable]
    public class RectTransformAnimation : SimpleAnimation
    {
        [SerializeField] public RectTransform rectTransformToAnimate;
        [SerializeField] public RectTransform originRectTransform;
        [SerializeField] public RectTransform destinationRectTransform;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public RectTransformAnimation() : this(null, null, null) { } 
        
        public RectTransformAnimation(RectTransform rectTransformToAnimate, RectTransform destinationRectTransform, RectTransform originRectTransform, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.rectTransformToAnimate = rectTransformToAnimate;
            this.originRectTransform = originRectTransform;
            this.destinationRectTransform = destinationRectTransform;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            rectTransformToAnimate.SetLerp(originRectTransform, destinationRectTransform, currentAnimationCurveValue);

            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            return rectTransformToAnimate;
        }
    }
    
}
