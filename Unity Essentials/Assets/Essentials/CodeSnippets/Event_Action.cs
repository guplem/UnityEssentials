using System;
using UnityEngine;

namespace Essentials.CodeSnippets
{
    // ReSharper disable once InconsistentNaming
    public class Event_Action : MonoBehaviour
    {
    
    
        /// <summary>
        /// Pointer to functions (and "event") with return type void and the specified params (int, string)
        /// </summary>
        private Action<int, string> nameOfEvent;

    
        private void Start()
        {
            //Subscribe the method MethodToCallOnEvent to the Action ("event pointer")
            nameOfEvent += MethodToCallOnEvent;
        }

    
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1)) // If pressed the '1' key on the top of the alphanumeric keyboard...
            {
                // Check if there is any functions subscribed to the Action ("event pointer")...
                if (nameOfEvent != null) 
                    // Call all functions subscribed to the event (with the given parameters)
                    nameOfEvent(420, "example string");
            
                //Short way: 
                /*
                 * NameOfEvent?.Invoke(420, "example string");
                 */
            }

        }
    

        /// <summary>
        /// Method subscribed to the event
        /// </summary>
        private void MethodToCallOnEvent(int firstParam, string secondParam)
        {
            Debug.Log($"MethodToCallOnEvent being called with parameters {firstParam} and {secondParam}");
        }
    
    
    }
}
