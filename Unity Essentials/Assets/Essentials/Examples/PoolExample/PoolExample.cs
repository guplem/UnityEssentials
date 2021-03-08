using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolExample : MonoBehaviour
{

    [SerializeField] private KeyCode keyToSpawn;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform parentForPooled;

    private Pool pool;
    private EasyRandom random;

    private void Start()
    {
        // The pool is created
        pool = new Pool(objectToSpawn, 3, false);
        
        random = new EasyRandom();
        
        Debug.Log($"Press '{keyToSpawn}' to spawn a new object from the pool.");
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToSpawn))
        {
            // Activate/Respawn/Move one game object each time 'Spawn' is called
            GameObject spawned = pool.Spawn(
                random.GetRandomVector3(-5, 5),   // Position
                Quaternion.identity,                                               // Rotation
                random.GetRandomVector3(0.5f,1.5f), // Scale
                parentForPooled                                                    // Parent
            );
        
            // Set a random name to the spawned GameObject 
            spawned.gameObject.name = "Random number name - " + random.GetRandomInt(0,1000).ToString();
        }
        
    }
}
