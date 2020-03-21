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
        [SerializeField] private float duration;
        [SerializeField] private AnimationCurve curve;

        public TransformAnimation() : this(null, null, null) { }
        public TransformAnimation(Transform transformToAnimate, Transform destination, Transform origin, float duration = 1f,
            Curve curve = Curve.EaseInOut, bool move = true, bool rotate = true, bool scale = true)
        {
            this.transformToAnimate = transformToAnimate;
            this.destinationTransform = destination;
            this.originTransform = origin;

            this.move = move;
            this.rotate = rotate;
            this.scale = scale;

            this.duration = duration;
            this.curve = SimpleAnimation.GetCurve(curve);
        }

        public override bool Step(float deltaTime)
        {
            elapsedTime += deltaTime;
            
            if (elapsedTime >= duration)
            {
                transformToAnimate.SetProperties(destinationTransform);
                return true;
            }
            else
            {
                transformToAnimate.SetLerp(originTransform.transform, destinationTransform, curve.Evaluate(elapsedTime/duration), move, rotate, scale);
                return false;
            }
        }
    }
}