using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolExample : MonoBehaviour
{

    [SerializeField] private KeyCode keyToSpawn;
    [SerializeField] private GameObject objectToSpawn;

    private Pool pool;

    private void Start()
    {
        pool = new Pool(objectToSpawn, 5, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToSpawn))
            pool.Spawn(Vector3.one, Quaternion.identity);
    }
}
