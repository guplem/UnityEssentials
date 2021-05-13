using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    bool rndBoolUnity { get { return (Random.value > 0.5f);  } }
    bool rndBoolEssentials { get { return (RandomEssentials.GetNew().GetRandomBool());  } }

    // Start is called before the first frame update
    void Start()
    {
        Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
