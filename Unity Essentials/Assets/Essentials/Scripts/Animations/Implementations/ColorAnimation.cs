using System;

namespace UnityEngine
{
    [Serializable]
    public class ColorAnimation : SimpleAnimation
    {
        [SerializeField] public Color colorToAnimate;
        [SerializeField] public Color originColor;
        [SerializeField] public Color destinationColor;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public ColorAnimation() : this(new Color(), new Color(), new Color()) { } 
        
        public ColorAnimation(Color colorToAnimate, Color destination, Color origin, float duration = 1f, Curve curve = Curve.EaseInOut)
        {
            this.colorToAnimate = colorToAnimate;
            this.originColor = origin;
            this.destinationColor = destination;
            

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
        }

        public override bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            bool endOfAnimation = base.Step(deltaTime, inverseIfMirror);
            
            colorToAnimate = Color.Lerp(originColor, destinationColor, currentAnimationValue);

            return endOfAnimation;
        }

        public override Object GetAnimatedObject()
        {
            Debug.LogWarning("Trying to obtain the animated object from a ColorAnimation. This action is not supported. Access the 'colorToAnimate' instead.");
            return null;
        }
    }
}
