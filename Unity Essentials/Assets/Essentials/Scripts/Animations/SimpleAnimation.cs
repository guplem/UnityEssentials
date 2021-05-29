using System;
using UnityEngine.Events;

namespace UnityEngine
{
    /// <summary>
    /// Base class to create simple animations of any element.
    /// <para>It can be used to animate cameras, int, floats, vectors, transforms, ...</para>
    /// <para></para>
    /// </summary>
    [Serializable] 
    public abstract class SimpleAnimation : ISimpleAnimation
    {
        /// <summary>
        /// The name of the animation
        /// </summary>
        [Tooltip("The name of the animation")]
        [SerializeField] public string name;
        /// <summary>
        /// The current moment/time of the animation. From 0 to duration.
        /// </summary>
        public float timeStamp { get => _timeStamp; protected set => _timeStamp = value; }
        [HideInInspector] private float _timeStamp;
        /// <summary>
        /// The progress of the animation. From 0 to 1.
        /// </summary>
        public float progress
        {
            get {
                if (!mirror)
                    return timeStamp/ duration;
                return 1 - (timeStamp/ duration);
            }
            set => SetProgress(value);
        }
        /// <summary>
        /// Determines how the animation behaves once finished. Should be executed only once? Start over? Go back?
        /// </summary>
        [Tooltip("Determines how the animation behaves once finished. Should be executed only once? Start over? Go back?")]
        [SerializeField] public WrapMode wrapMode;
        /// <summary>
        /// Should the animation be played from end to start instead of from start to end?
        /// </summary>
        [Tooltip("Should the animation be played from end to start instead of from start to end?")]
        [SerializeField] public bool mirror;
        /// <summary>
        /// How much should the animation last?
        /// </summary>
        [Tooltip("How much should the animation last?")]
        [SerializeField] public float duration;
        /// <summary>
        /// The curve of the animation over time.
        /// </summary>
        [Tooltip("The curve of the animation over time. The bottom is the start state and the top is end state.")]
        [SerializeField] public AnimationCurve curve;
        /// <summary>
        /// The value of the animation at the current time. 0 means start state. 1 means end state.
        /// </summary>
        public float currentAnimationCurveValue => curve.Evaluate(timeStamp / duration);
        /// <summary>
        /// Events executed every time the animation advances.
        /// </summary>
        [Tooltip("Events executed every time the animation advances")]
        [SerializeField] public UnityEvent onStep;
        /// <summary>
        /// Events executed when the animation finishes.
        /// </summary>
        [Tooltip("Events executed when the animation finishes")]
        [SerializeField] public UnityEvent onFinish;

        
        /// <summary>
        /// How the animation behaves once finished. Should be executed only once? Start over? Go back?
        /// </summary>
        public enum WrapMode
        {
            Once,
            Loop,
            PingPong
        }
        
        /// <summary>
        /// Go forward or backwards in the animation.
        /// </summary>
        /// <param name="deltaTime">The elapsed time between the last step and the current one.</param>
        /// <param name="inverseIfMirror">If true, the delta time of the step will be inverted if the animation is set to mirror.</param>
        /// <returns>True if the animation should have ended. False if the animation should still in progress.</returns>
        public virtual bool Step(float deltaTime, bool inverseIfMirror = true)
        {
            if (!mirror || !inverseIfMirror)
                timeStamp += deltaTime;
            else if (mirror)
                timeStamp -= deltaTime;
            else
                Debug.LogError("Unexpected Step call for a SimpleAnimation.");

            onStep?.Invoke();
            
            if ( ((timeStamp >= duration) && !mirror) || ((timeStamp <= 0) && mirror) )
            {
                if (!mirror)
                    timeStamp = duration;
                else
                    timeStamp = 0;
                
                onFinish?.Invoke();

                switch (wrapMode)
                {

                    case WrapMode.Once:
                        break;
                    case WrapMode.Loop:
                        SetProgress(0f);
                        break;
                    case WrapMode.PingPong:
                        mirror = !mirror;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
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

        /// <summary>
        /// Sets the animation at the given progress. 
        /// </summary>
        /// <param name="progress">The progress of the animation [0,1]</param>
        public virtual void SetProgress(float progress)
        {
            if (mirror) progress = 1 - progress;
            float desiredTime = progress * duration;
            Step(desiredTime - timeStamp, !mirror);
        }

        /// <summary>
        /// Returns the UnityEngine.Object animated. If not applicable, return null.
        /// </summary>
        public abstract UnityEngine.Object GetAnimatedObject(bool displayWarningIfNotApplicable = true);

    }
    
    /// <summary>
    /// Predefined curves for the SimpleAnimations
    /// </summary>
    public enum Curve
    {
        Linear,
        EaseInOut
    }
    
}