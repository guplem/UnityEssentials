using System;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class DoOnce
    {
        [SerializeField] public UnityEvent calledEvent;
        [NonSerialized] public bool eventInvoked = false;

        public DoOnce(UnityEvent calledEvent)
        {
            this.calledEvent = calledEvent;
        }

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
