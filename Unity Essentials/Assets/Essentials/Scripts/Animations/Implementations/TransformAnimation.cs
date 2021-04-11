using System;

namespace UnityEngine
{
    [Serializable]
    public class TransformAnimation : SimpleAnimation
    {
        [SerializeField] public Transform transformToAnimate;
        [SerializeField] public Transform originTransform;
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