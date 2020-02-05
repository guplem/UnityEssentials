using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public abstract class WorldAnimation
    {
        public float elapsedTime { get; protected set; }

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

        public abstract bool Step(float deltaTime);
    }
    
    public enum Curve
    {
        Linear,
        EaseInOut
    }
}