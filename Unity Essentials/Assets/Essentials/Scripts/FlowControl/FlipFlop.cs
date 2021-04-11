using System;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class FlipFlop
    {
        
        [SerializeField] public UnityEvent a;
        [SerializeField] public UnityEvent b;
        [HideInInspector] public UnityEvent nextEvent;
        
        public void Invoke()
        {
            if (nextEvent.GetPersistentEventCount() <= 0)
                nextEvent = a;

            nextEvent?.Invoke();
            
            if (nextEvent == a)
                nextEvent = b;
            else
                nextEvent = a;
        }
        
    }
    
}