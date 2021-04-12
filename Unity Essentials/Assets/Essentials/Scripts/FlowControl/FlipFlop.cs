using System;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class FlipFlop
    {
        
        [SerializeField] public UnityEvent firstEvent;
        [SerializeField] public UnityEvent secondEvent;
        [NonSerialized] public UnityEvent nextEvent = null;


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