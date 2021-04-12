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
