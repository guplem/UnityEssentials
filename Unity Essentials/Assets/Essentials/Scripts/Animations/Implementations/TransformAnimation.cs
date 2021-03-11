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
        
        public TransformAnimation(Transform transformToAnimate, Transform destination, Transform origin, float duration = 1f, Curve curve = Curve.EaseInOut)
        {
            this.transformToAnimate = transformToAnimate;
            this.originTransform = origin;
            this.destinationTransform = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
        }

        public override bool Step(float deltaTime)
        {
            bool endOfAnimation = base.Step(deltaTime);
            
            transformToAnimate.SetLerp(originTransform.transform, destinationTransform, curve.Evaluate(timeStamp / duration));

            return endOfAnimation;
        }

        public override Object GetAnimatedObject()
        {
            return transformToAnimate;
        }
    }
}