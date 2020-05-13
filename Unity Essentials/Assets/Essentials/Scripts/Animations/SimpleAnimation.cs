﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public abstract class SimpleAnimation : ISimpleAnimation
    {
        public float timeStamp { get; protected set; }
        [Space(30, order = 0)]
        [Header("Animation configuration", order = 1)]
        [SerializeField] public bool mirror;
        [SerializeField] public float duration;
        [SerializeField] public AnimationCurve curve;
        [SerializeField] public UnityEvent OnFinish;

        /// <summary>
        /// Go forward or backwards in the animation.
        /// </summary>
        /// <param name="deltaTime">The elapsed time between the last step and the current one.</param>
        /// <returns>True if the animation should have ended. False if the animation should still in progress.</returns>
        public virtual bool Step(float deltaTime)
        {
            if (!mirror)
                timeStamp += deltaTime;
            else
                timeStamp -= deltaTime;

            if ( ((timeStamp >= duration) && !mirror) || ((timeStamp <= 0) && mirror) )
            {
                OnFinish?.Invoke();
                return ((timeStamp >= duration) && !mirror) || ((timeStamp <= 0) && mirror); // Double evaluation to avoid bugs with modifications on the event invoked.
            }
            
            return false;
        }

        /// <summary>
        /// Sets the time stamps of the animation to the beginning (the behaviour changes depending on if the animation is set as mirror or not).
        /// </summary>
        public void Reset()
        {
            timeStamp = !mirror? 0f : duration;
        }


        public override string ToString()
        {
            return "Current time: " + timeStamp + "/" + duration + ". Mirror: " + mirror;
        }


        /// <summary>
        /// Obtains the desired type of animation curve with a duration of 1 (starting on 0), the start value being 0 and the end value being 1
        /// </summary>
        /// <param name="curve">THe desired type of curve.</param>
        /// <returns>The desired type of animation curve with a duration of 1 (starting on 0), the start value being 0 and the end value being 1</returns>
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
    }
    public enum Curve
    {
        Linear,
        EaseInOut
    }
    
    
}