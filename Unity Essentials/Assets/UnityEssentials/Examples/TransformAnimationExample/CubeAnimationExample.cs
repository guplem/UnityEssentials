using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimationExample : MonoBehaviour
{

    [SerializeField] private WorldAnimationsManager animationsManager;
    [SerializeField] private Transform destination;
    
    void Start()
    {
        Debug.Log("Press G to play the animation inserted by code");
        GameObject kkk = new GameObject();
        Debug.Log("Press H to play the animation inserted trough the inspector");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Playing animation inserted by code");
            animationsManager.Play(new TransformAnimation(this.transform, destination, 1f, Curve.Linear, true, true, true));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.LogWarning("TODO animation by code");
        }
    }
}
