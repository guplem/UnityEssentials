using System.Collections;
using UnityEngine;

namespace Essentials.Examples.Pool.PoolBig
{
    public class PoolExampleBig : MonoBehaviour
    {

        [SerializeField] private KeyCode keyToSpawn;
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private Transform parentForPooled;
        [SerializeField] private float timeBetweenLoadingInstances = 1;

        private PoolEssentials poolEssentials;
        private UnityEngine.RandomEssentials randomEssentials;
        private IEnumerator coroutineHolder; // Keeps track of the coroutine

        private void Start()
        {
            // The pool is created
            poolEssentials = new UnityEngine.PoolEssentials(objectToSpawn, 100, false);
        
            randomEssentials = new UnityEngine.RandomEssentials();
        
            Debug.Log($"Press '{keyToSpawn}' to spawn a new object from the pool.");
        
            //Assign the coroutine to the holder
            coroutineHolder = LoadingCoroutine();
            //Run the coroutine
            StartCoroutine(coroutineHolder);
        }

        void Update()
        {
            if (Input.GetKeyDown(keyToSpawn))
            {
                // Activate/Respawn/Move one game object each time 'Spawn' is called
                GameObject spawned = poolEssentials.Spawn(
                    randomEssentials.GetRandomVector3(-5, 5),   // Position
                    Quaternion.identity,                                               // Rotation
                    randomEssentials.GetRandomVector3(0.5f,1.5f), // Scale
                    parentForPooled                                                    // Parent
                );
        
                // Set a random name to the spawned GameObject 
                spawned.gameObject.name = "Random number name - " + randomEssentials.GetRandomInt(0,1000).ToString();
            }
        
        }
    
        // Method/Corroutine used as example
        private IEnumerator LoadingCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenLoadingInstances);
            
                poolEssentials.Load(1, parentForPooled);
            }
        }
    }
}
