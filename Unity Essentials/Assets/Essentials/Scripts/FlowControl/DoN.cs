using System;
using UnityEngine.Events;

namespace UnityEngine
{
    /// <summary>
    /// Allows the limitation of how many times an event can be executed.
    /// </summary>
    [Serializable]
    public class DoN
    {
        /// <summary>
        /// The amount of times the events can be invoked.
        /// </summary>
        [Tooltip("The amount of times the events can be invoked")]
        [SerializeField] public int invokingTimes = 1;
        /// <summary>
        /// How many times the events have been invoked.
        /// </summary>
        [NonSerialized] public int invokedTimes = 0;
        /// <summary>
        /// The events called at every invoke.
        /// </summary>
        [Tooltip("The events called at every invoke")]
        [SerializeField] public UnityEvent calledEvent;

        public DoN(UnityAction unityAction, int invokingTimes)
        {
            UnityEvent unityEvent = new UnityEvent();
            unityEvent.AddListener(unityAction);
            this.calledEvent = unityEvent;
            this.invokingTimes = invokingTimes;
        }
        
        public DoN(UnityEvent calledEvent, int invokingTimes)
        {
            this.calledEvent = calledEvent;
            this.invokingTimes = invokingTimes;
        }

        /// <summary>
        /// Invokes all registered events callbacks (runtime and persistent).
        /// </summary>
        public void Invoke()
        {
            if (invokedTimes < invokingTimes)
            {
                calledEvent?.Invoke();
                invokedTimes++;
            }
        }
        
    }
    
}
