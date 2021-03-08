using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolExampleBig : MonoBehaviour
{

    [SerializeField] private KeyCode keyToSpawn;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform parentForPooled;

    private Pool pool;
    private EasyRandom random;
    private IEnumerator coroutineHolder; // Keeps track of the coroutine

    private void Start()
    {
        // The pool is created
        pool = new Pool(objectToSpawn, 100, false);
        
        random = new EasyRandom();
        
        Debug.Log($"Press '{keyToSpawn}' to spawn a new object from the pool.");
        
        //Assign the coroutine to the holder
        coroutineHolder = CourutineMethod();
        //Run the coroutine
        StartCoroutine(coroutineHolder);
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
    
    // Method/Corroutine used as example
    private IEnumerator CourutineMethod()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Loaded");
            pool.Load(1, parentForPooled);
        }
    }
}
