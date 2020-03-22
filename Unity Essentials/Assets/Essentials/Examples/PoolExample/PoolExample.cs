using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolExample : MonoBehaviour
{

    [SerializeField] private KeyCode keyToSpawn;
    [SerializeField] private GameObject objectToSpawn;

    private Pool pool;
    private EasyRandom random;

    private void Start()
    {
        // The pool is created
        pool = new Pool(objectToSpawn, 5, false);
        
        random = new EasyRandom();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToSpawn))
            // Activate one game object each time 'Spawn' is called
            pool.Spawn(random.GetRandomVector3(-5, 5), Quaternion.identity, 
                random.GetRandomVector3(0.5f,1.5f));
    }
}
