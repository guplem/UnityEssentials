using System;
using UnityEngine.Events;

namespace UnityEngine
{
    [Serializable]
    public class DoN
    {
        [SerializeField] public int invokingTimes = 3;
        [NonSerialized] public int invokedTimes = 0;
        [SerializeField] public UnityEvent calledEvent;

        public DoN(UnityEvent calledEvent, int invokingTimes)
        {
            this.calledEvent = calledEvent;
            this.invokingTimes = invokingTimes;
        }

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
