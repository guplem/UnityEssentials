using System;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class DoOnce
    {
        [SerializeField] public UnityEvent calledEvent;
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
