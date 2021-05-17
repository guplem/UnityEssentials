using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Test : MonoBehaviour
{

    [SerializeField] private GameObject spawned;
    [SerializeField] private float radius;
    [SerializeField] private Vector2 center;

    private void Update()
    {
        var i = UnityEngine.Random.insideUnitSphere;
        
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 pos = RandomEssentials.GetNew().GetPointInsideCircle(center, radius);
            Debug.Log($"INSTANTIATING AT {pos}");
            Instantiate(spawned, pos, Quaternion.identity);
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(center,radius);
    }

}
