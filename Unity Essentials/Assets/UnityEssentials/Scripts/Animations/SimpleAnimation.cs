using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    
    public abstract class SimpleAnimation : ISimpleAnimation
    {
        public float timeStamp { get; protected set; }
        [SerializeField] public bool mirror;
        [SerializeField] protected float duration;
        [SerializeField] protected AnimationCurve curve;

        public static AnimationCurve GetCurve(Curve curve)
        {
            switch (curve)
            {
                case Curve.Linear:
                    return AnimationCurve.Linear(0, 0, 1, 1);
                case Curve.EaseInOut:
                    return AnimationCurve.EaseInOut(0, 0, 1, 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(curve), curve, null);
            }
        }

        /// <summary>
        /// Go forward or backwards in the animation.
        /// </summary>
        /// <param name="exclusiveMaximum">The exclusive maximum value than can be obtained.</param>
        /// <param name="inclusiveMinimum">The minimum value that can be obtained.</param>
        /// <param name="variancePerStep">The value added to the int every time that the method is called.</param>
        /// <returns>Returns a the result of adding the 'variancePerStep' (default to 1f) to the original integer. The value is always between the minimum (inclusive, default to 0f) and the maximum (exclusive).</returns>
        public virtual bool Step(float deltaTime)
        {
            if (!mirror)
                timeStamp += deltaTime;
            else
                timeStamp -= deltaTime;

            return ((timeStamp >= duration) && !mirror) || ((timeStamp <= 0) && mirror);
        }

        public void Reset()
        {
            timeStamp = !mirror? 0f : duration;
        }
    }
    
    public enum Curve
    {
        Linear,
        EaseInOut
    }
    
}