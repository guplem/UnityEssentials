using System;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class FlipFlop
    {
        
        [SerializeField] public UnityEvent firstEvent;
        [SerializeField] public UnityEvent secondEvent;
        [HideInInspector] public UnityEvent nextEvent;

        public FlipFlop(UnityEvent firstEvent, UnityEvent secondEvent)
        {
            this.firstEvent = firstEvent;
            this.secondEvent = secondEvent;
        }

        public void Invoke()
        {
            if (nextEvent.GetPersistentEventCount() <= 0)
                nextEvent = firstEvent;

            nextEvent?.Invoke();
            
            if (nextEvent == firstEvent)
                nextEvent = secondEvent;
            else
                nextEvent = firstEvent;
        }
        
    }
    
}