using System;

namespace UnityEngine
{
    [Serializable]
    public class IntAnimation : SimpleAnimation
    {
        [NonSerialized] public int intToAnimate;
        [SerializeField] public int originInt;
        [SerializeField] public int destinationInt;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public IntAnimation() : this(0, 0, 0) { } 
        
        public IntAnimation(int intToAnimate, int destination, int origin, float duration = 1f, Curve curve = Curve.EaseInOut, WrapMode wrapMode = WrapMode.Once)
        {
            this.intToAnimate = intToAnimate;
            this.originInt = origin;
            this.destinationInt = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
            this.wrapMode = wrapMode;
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            //intToAnimate = Color.Lerp(originInt, destinationInt, currentAnimationValue);
            intToAnimate = (int)Mathf.Lerp(originInt, destinationInt, currentAnimationCurveValue);
            
            return endOfAnimation;
        }

        public override Object GetAnimatedObject(bool displayWarningIfNotApplicable)
        {
            if (displayWarningIfNotApplicable)
                Debug.LogWarning("Trying to obtain the animated object from a IntAnimation. This action is not supported. Access the 'intToAnimate' instead.");
            return null;
        }
    }
}
