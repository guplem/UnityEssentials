using System.Collections;
using UnityEngine;

namespace Essentials.CodeSnippets
{
    public class Coroutine : MonoBehaviour
    {
        /// <summary>
        /// Keeps track of the coroutine
        /// </summary>
        private IEnumerator coroutineHolder;

        
        /// <summary>
        /// Logic to start and stop the coroutine
        /// </summary>
        private void Update()
        {
            
            //START
            if (Input.GetKeyDown(KeyCode.Alpha1)) // If pressed the '1' key on the top of the alphanumeric keyboard...
            {
                StartCustomCoroutine(); // Custom method to start the coroutine
            }

            // STOP
            if (Input.GetKeyDown(KeyCode.Alpha2)) // If pressed the '2' key on the top of the alphanumeric keyboard...
            {
                StopCustomCoroutine(); // Custom method to stop the coroutine
            }

            // STOP ALL
            if (Input.GetKeyDown(KeyCode.Alpha3)) // If pressed the '3' key on the top of the alphanumeric keyboard...
            {
                StopAllCoroutines();
            }

        }
        
        
        /// <summary>
        /// Custom method to start the coroutine
        /// </summary>
        private void StartCustomCoroutine()
        {

            // Ensure that we are not going to lose the track of a previous coroutine 
            // If we lose it, we'll not be able to stop it.
            if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);

            //Assign the coroutine to the holder
            coroutineHolder = CoroutineMethod();
            //Run the coroutine
            StartCoroutine(coroutineHolder);


            //If you are not going yo need to stop the coroutine, you do not need the "coroutineHolder":
            /*
             * StartCoroutine(CoroutineMethod());
            */
        }
        
        
        /// <summary>
        /// Custom method to stop the coroutine
        /// </summary>
        private void StopCustomCoroutine()
        {
            if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);
            
            //It is recommended to clear the holder
            coroutineHolder = null;
        }

        
        /// <summary>
        /// Method of the coroutine used as example
        /// </summary>
        private IEnumerator CoroutineMethod()
        {
            //Loop forever
            while (true) 
            {
                Debug.Log("Running CoroutineMethod");

                // Wait before continuing the execution
                yield return new WaitForSeconds(0.1f);

                //It you dont want the game's time scale to affect the wait time use this instead:
                /*
                 * yield return new WaitForSecondsRealtime(0.1f);
                */

                Debug.Log("After yield/waiting");
            }

        }

        
    }
}
