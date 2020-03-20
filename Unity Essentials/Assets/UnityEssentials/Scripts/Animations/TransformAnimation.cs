using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public class TransformAnimation : WorldAnimation
    {
        private DummyTransform originalTransform;
        private Transform transformToAnimate;
        private Transform destinationTransform;
        private readonly bool move;
        private readonly bool scale;
        private readonly bool rotate;
        private readonly float duration;
        private readonly Curve curve;

        public TransformAnimation(Transform transformToAnimate, Transform destination, float duration = 1f, Curve curve = Curve.Linear, bool rotate = true, bool scale = true, bool move = true)
        {
            originalTransform = new DummyTransform(transformToAnimate);
            
            this.transformToAnimate = transformToAnimate;
            this.destinationTransform = destination;
            
            this.move = move;
            this.scale = scale;
            this.rotate = rotate;
            
            this.duration = duration;
            this.curve = curve;
        }


        public override bool Step(float deltaTime)
        {
            // TODO --> Actual animation
            
            
            elapsedTime += deltaTime;
            Debug.Log(elapsedTime);

            
            if (elapsedTime >= duration)
            {
                transformToAnimate.SetProperties(destinationTransform);
                return true;
            }
            else return false;
        }
    }
}