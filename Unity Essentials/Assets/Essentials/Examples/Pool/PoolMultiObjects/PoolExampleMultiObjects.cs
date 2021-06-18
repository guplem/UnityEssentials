using UnityEngine;

namespace Essentials.Examples.Pool.PoolMultiObjects
{
    public class PoolExampleMultiObjects : MonoBehaviour
    {

        [SerializeField] private KeyCode keyToSpawn;
        [SerializeField] private GameObject[] objectsToSpawn;
        [SerializeField] private Transform parentForPooled;
        [SerializeField] private bool randomInstantiationSequence;

        private PoolEssentials poolEssentials;
        private UnityEngine.RandomEssentials randomEssentials;

        private void Start()
        {
            // The pool is created
            poolEssentials = new UnityEngine.PoolEssentials(objectsToSpawn, 10, Vector3.zero, Quaternion.identity, false, randomInstantiationSequence);
        
            randomEssentials = new UnityEngine.RandomEssentials();
        
            Debug.Log($"Press '{keyToSpawn}' to spawn a new object from the pool.");
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
    
    }
}
