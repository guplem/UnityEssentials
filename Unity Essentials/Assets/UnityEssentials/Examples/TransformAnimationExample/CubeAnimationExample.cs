using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimationExample : MonoBehaviour
{

    [SerializeField] private SimpleAnimationsManager simpleAnimationsManager;
    [Space]
    [SerializeField] private Transform origin;
    [SerializeField] private Transform destination;
    
    
    void Start()
    {
        Debug.Log("Press G to play the animation inserted by code");
        Debug.Log("Press H to play the animation inserted trough the inspector");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Creating and playing a new animation trough code.");
            simpleAnimationsManager.Play(new TransformAnimation(this.transform, destination, origin, 1f, Curve.Linear, true, true, true));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Playing the animation configured tough the inspector.");
            simpleAnimationsManager.Play(0);
        }
    }
}
