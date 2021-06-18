using UnityEngine;
using UnityEngine.Serialization;

namespace Essentials.Examples.Pool.PoolSmall
{
    public class PoolExampleSmall : MonoBehaviour
    {

        [SerializeField] private KeyCode keyToSpawn;
        [FormerlySerializedAs("pool")]
        [Space]
        [SerializeField] private PoolEssentials poolEssentials;
    
        private UnityEngine.RandomEssentials randomEssentials;

        private void Start()
        {
            randomEssentials = new UnityEngine.RandomEssentials();
        
            Debug.Log($"Press '{keyToSpawn}' to spawn a new object from the pool.");
        }

        void Update()
        {
            if (Input.GetKeyDown(keyToSpawn))
            {
                // Activate/Respawn/Move one game object each time 'Spawn' is called
                GameObject spawned = poolEssentials.Spawn(
                    randomEssentials.GetRandomVector3(-5, 5),  // Position
                    Quaternion.identity,                                              // Rotation
                    randomEssentials.GetRandomVector3(0.5f,1.5f) // Scale
                );
        
                // Set a random name to the spawned GameObject 
                spawned.gameObject.name = "Random number name - " + randomEssentials.GetRandomInt(0,1000).ToString();
            }
        
        }
    }
}
