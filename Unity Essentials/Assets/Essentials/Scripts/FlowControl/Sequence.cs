using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
    /// <summary>
    /// Allows you to execute different events every time the Invoke method of this is class called.
    /// </summary>
    [Serializable]
    public class Sequence
    {
        /// <summary>
        /// Should the order of execution of the events be random? If false, the order will be the same as in the events array.
        /// </summary>
        [Tooltip("Should the order of execution of the events be random? If false, the order will be the same as in the events array.")]
        [SerializeField] public bool randomizeOrder;
        /// <summary>
        /// The randomness generation object to choose the next event to execute. 
        /// </summary>
        [NonSerialized] private RandomEssentials random = new RandomEssentials();
        /// <summary>
        /// Each of the events to execute at every call of the Invoke method.
        /// </summary>
        [Tooltip("Each of the events to execute at every call of the Invoke method.")]
        [SerializeField] public UnityEvent[] events;
        /// <summary>
        /// The next UnityEvent to be executed. If null, firstEvent is going to be executed next .
        /// </summary>
        [NonSerialized] private UnityEvent nextEvent = null;
        /// <summary>
        /// The index of the next UnityEvent to be executed.
        /// </summary>
        [NonSerialized] private int nextEventIndex = 0;

        public Sequence(UnityAction[] actions, bool randomizeOrder = false, int randomizationSeed = -1) 
        {
            List<UnityEvent> events = new List<UnityEvent>();
           
            foreach (UnityAction unityAction in actions)
            {
                UnityEvent unityEvent = new UnityEvent();
                unityEvent.AddListener(unityAction);
                events.Add(unityEvent);
            }
            
            this.events = events.ToArray();
            this.randomizeOrder = randomizeOrder;
            
            if (randomizationSeed == -1)
                random = new RandomEssentials();
            else
                random = new RandomEssentials(randomizationSeed);
        }
        
        public Sequence(UnityEvent[] events, bool randomizeOrder = false, int randomizationSeed = -1)
        {
            this.events = events;
            this.randomizeOrder = randomizeOrder;
            
            if (randomizationSeed == -1)
                random = new RandomEssentials();
            else
                random = new RandomEssentials(randomizationSeed);
        }

        public void Invoke()
        {
            if (randomizeOrder && random == null)
                random = new RandomEssentials();
            
            if (nextEvent == null)
            {
                if (!randomizeOrder)
                {
                    nextEvent = events[0];
                }
                else
                {
                    nextEventIndex = random.GetRandomInt(events.Length);
                    nextEvent = events[nextEventIndex];
                }
            }

            nextEvent?.Invoke();
            
            if (!randomizeOrder)
                nextEventIndex = nextEventIndex.GetLooped(events.Length);
            else
                nextEventIndex = random.GetRandomInt(events.Length);
            
            nextEvent = events[nextEventIndex];
        }
        
    }
    
}