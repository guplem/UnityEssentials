using System;
using UnityEngine.Events;

namespace UnityEngine
{
    /// <summary>
    /// Limits the execution of an event so it can only be executed one time.
    /// </summary>
    [Serializable]
    public class DoOnce
    {
        /// <summary>
        /// The events called at the invoke.
        /// </summary>
        [Tooltip("The events called at the invoke")]
        [SerializeField] public UnityEvent calledEvent;
        /// <summary>
        /// Weather or not the even has been executed already.
        /// </summary>
        [NonSerialized] public bool eventInvoked = false;

        public DoOnce(UnityAction unityAction)
        {
            UnityEvent unityEvent = new UnityEvent();
            unityEvent.AddListener(unityAction);
            this.calledEvent = unityEvent;
        }
        
        public DoOnce(UnityEvent calledEvent)
        {
            this.calledEvent = calledEvent;
        }

        /// <summary>
        /// Invokes all registered events callbacks (runtime and persistent).
        /// </summary>
        public void Invoke()
        {
            if (!eventInvoked)
            {
                calledEvent?.Invoke();
                eventInvoked = true;
            }
        }
        
    }
    
}
