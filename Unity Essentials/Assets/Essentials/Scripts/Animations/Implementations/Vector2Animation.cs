using System;

namespace UnityEngine
{
    [Serializable]
    public class Vector2Animation : SimpleAnimation
    {
        [NonSerialized] public Vector2 vector2ToAnimate;
        [SerializeField] public Vector2 originVector2;
        [SerializeField] public Vector2 destinationVector2;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public Vector2Animation() : this(Vector2.zero, Vector2.zero, Vector2.zero) { } 
        
        public Vector2Animation(Vector2 vector2ToAnimate, Vector2 destination, Vector2 origin, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.vector2ToAnimate = vector2ToAnimate;
            this.originVector2 = origin;
            this.destinationVector2 = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            vector2ToAnimate = Vector2.Lerp(originVector2, destinationVector2, currentAnimationCurveValue);
            
            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            if (displayWarningIfNotApplicable)
                Debug.LogWarning("Trying to obtain the animated object from a Vector2Animation. This action is not supported. Access the 'vector2ToAnimate' instead.");
            return null;
        }
    }
}
