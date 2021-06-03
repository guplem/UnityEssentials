using UnityEngine;

namespace Essentials.CodeSnippets
{
    // ReSharper disable once InconsistentNaming
    public class Event_Delegate : MonoBehaviour
    {
    
    
        /// <summary>
        /// Pointer to methods (and "event") with return type float and the specified params (int, string)
        /// </summary>
        public delegate float NameOfDelegate(int intParam, string stringParam);
    
    
        /// <summary>
        /// Name of the event created to call the functions with the characteristics of the given pointer/delegate 
        /// </summary>
        public event NameOfDelegate NameOfEvent;


        private void Start()
        {
            //Subscribe the method MethodToCallOnEvent to the event
            NameOfEvent += MethodToCallOnEvent;
        }

    
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1)) // If pressed the '1' key on the top of the alphanumeric keyboard...
            {
                // Check if there is any functions subscribed to the Delegate ("event pointer")...
                if (NameOfEvent != null) 
                    // Call all functions subscribed to the event (with the given parameters)
                    //Note: The return of the execution of the vent is the return of the last method added to the delegate (event pointer).
                    NameOfEvent(69, "example string");  
            
                //Short way: 
                /*
                 * NameOfEvent?.Invoke(69, "example string");
                 */
            }

        }


        // Method subscribed to the event
        private float MethodToCallOnEvent(int firstParam, string secondParam)
        {
            Debug.Log($"MethodToCallOnEvent being called with parameters {firstParam} and {secondParam}");
            return 4.20f;
        }
    
    
    }
}
