using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolExampleSmall : MonoBehaviour
{

    [SerializeField] private KeyCode keyToSpawn;
    [Space]
    [SerializeField] private Pool pool;
    
    private EasyRandom random;

    private void Start()
    {
        random = new EasyRandom();
        
        Debug.Log($"Press '{keyToSpawn}' to spawn a new object from the pool.");
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToSpawn))
        {
            // Activate/Respawn/Move one game object each time 'Spawn' is called
            GameObject spawned = pool.Spawn(
                random.GetRandomVector3(-5, 5),  // Position
                Quaternion.identity,                                              // Rotation
                random.GetRandomVector3(0.5f,1.5f) // Scale
            );
        
            // Set a random name to the spawned GameObject 
            spawned.gameObject.name = "Random number name - " + random.GetRandomInt(0,1000).ToString();
        }
        
    }
}
