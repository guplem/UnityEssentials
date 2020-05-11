using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    [Serializable]
    public class RectTransformAnimation : SimpleAnimation
    {
        [SerializeField] private RectTransform rectTransformToAnimate;
        [SerializeField] private RectTransform originRectTransform;
        [SerializeField] private RectTransform destinationRectTransform;
        [SerializeField] private bool move;
        [SerializeField] private bool rotate;
        [SerializeField] private bool scale;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public RectTransformAnimation() : this(null, null, null) { } 
        
        public RectTransformAnimation(RectTransform rectTransformToAnimate, RectTransform destinationRectTransform, RectTransform originRectTransform, float duration = 1f, Curve curve = Curve.EaseInOut, bool move = true, bool rotate = true, bool scale = true)
        {
            this.rectTransformToAnimate = rectTransformToAnimate;
            this.originRectTransform = originRectTransform;
            this.destinationRectTransform = destinationRectTransform;

            this.move = move;
            this.rotate = rotate;
            this.scale = scale;

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
        }

        public override bool Step(float deltaTime)
        {
            bool endOfAnimation = base.Step(deltaTime);
            
            //TODO: animate RectTransforms
            rectTransformToAnimate.SetLerp(originRectTransform.transform, destinationRectTransform, curve.Evaluate(timeStamp / duration), move, rotate, scale);

            return endOfAnimation;
        }
    }
    
}
