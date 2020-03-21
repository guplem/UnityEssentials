using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    [Serializable]
    public class TransformAnimation : SimpleAnimation
    {
        [SerializeField] private Transform transformToAnimate;
        [SerializeField] private Transform originTransform;
        [SerializeField] private Transform destinationTransform;
        [SerializeField] private bool move;
        [SerializeField] private bool rotate;
        [SerializeField] private bool scale;

        // It is mandatory to have a parameterless constructor to properly work with the SimpleAnimationsManager component in the inspector.
        public TransformAnimation() : this(null, null, null) { } 
        
        public TransformAnimation(Transform transformToAnimate, Transform destination, Transform origin, float duration = 1f, Curve curve = Curve.EaseInOut, bool move = true, bool rotate = true, bool scale = true)
        {
            this.transformToAnimate = transformToAnimate;
            this.originTransform = origin;
            this.destinationTransform = destination;

            this.move = move;
            this.rotate = rotate;
            this.scale = scale;

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
        }

        public override bool Step(float deltaTime)
        {
            bool endOfAnimation = base.Step(deltaTime);
            
            transformToAnimate.SetLerp(originTransform.transform, destinationTransform, curve.Evaluate(timeStamp / duration), move, rotate, scale);

            return endOfAnimation;
        }
    }
}