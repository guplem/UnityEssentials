using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimationExample : MonoBehaviour
{

    [SerializeField] private SimpleAnimationsManager simpleAnimationsManager;
    [Space]
    [SerializeField] private Transform origin;
    [SerializeField] private Transform destination;


    private TransformAnimation codeAnimation = null;
    
    void Start()
    {
        codeAnimation = new TransformAnimation(this.transform, destination, origin, 1f, Curve.Linear, true, true, true);
        
        Debug.Log("Press G to play and T to stop the animation inserted by code");
        Debug.Log("Press H to play and Y to stop the animation inserted trough the inspector");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Playing an animation configured trough code.");
            simpleAnimationsManager.Play(codeAnimation);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Playing an animation configured trough the inspector.");
            simpleAnimationsManager.Play(0);
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Stopping an animation configured trough code.");
            simpleAnimationsManager.Stop(codeAnimation);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Stopping an animation configured trough the inspector.");
            simpleAnimationsManager.Stop(0);
        }
    }
}
