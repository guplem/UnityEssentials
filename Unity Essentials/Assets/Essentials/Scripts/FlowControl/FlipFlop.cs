using System;
using UnityEngine.Events;

namespace UnityEngine
{
    /// <summary>
    /// Allows you to alternate between the execution of two events with each call of the Invoke method.
    /// </summary>
    [Serializable]
    public class FlipFlop
    {
        /// <summary>
        /// A set of events to execute together.
        /// </summary>
        [Tooltip("A set of events to execute together")]
        [SerializeField] public UnityEvent firstEvent;
        /// <summary>
        /// A set of events to execute together.
        /// </summary>
        [Tooltip("A set of events to execute together")]
        [SerializeField] public UnityEvent secondEvent;
        /// <summary>
        /// The next UnityEvent to be executed. If null, firstEvent is going to be executed next .
        /// </summary>
        [NonSerialized] private UnityEvent nextEvent = null;

        public FlipFlop(UnityAction firstAction, UnityAction secondAction)
        {
            UnityEvent firstEventConstructor = new UnityEvent();
            firstEventConstructor.AddListener(firstAction);
            
            UnityEvent secondEventConstructor = new UnityEvent();
            secondEventConstructor.AddListener(secondAction);

            this.firstEvent = firstEventConstructor;
            this.secondEvent = secondEventConstructor;
        }
        
        public FlipFlop(UnityEvent firstEvent, UnityEvent secondEvent)
        {
            this.firstEvent = firstEvent;
            this.secondEvent = secondEvent;
        }

        /// <summary>
        /// Invokes the firstEvent or the secondEvent alternating.
        /// </summary>
        public void Invoke()
        {
            nextEvent ??= firstEvent;

            nextEvent?.Invoke();
            
            if (nextEvent == firstEvent)
                nextEvent = secondEvent;
            else
                nextEvent = firstEvent;
        }
        
    }
    
}