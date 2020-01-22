using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugProExample : MonoBehaviour
{

    
    void Start()
    {
        DebugPro.Log(" ======== DEBUGGING ARRAY: ======== ");
        int[] arrayInts = {1, 2, 3, 4, 5, 6, 7, 8};
        DebugPro.Log(arrayInts); // Array with no message
        DebugPro.Log(arrayInts, "Message printed before the list array: "); // Array with message
        DebugPro.Log(arrayInts, "Message printed before the list array: ", this);  // Array with message and referencing the component's gameObject
        

        DebugPro.Log(" ======== DEBUGGING LIST: ======== ");
        List<int> listInts = arrayInts.ToList();
        DebugPro.Log(listInts, ""); // List with no message
        DebugPro.Log(listInts, "Message printed before the list list: "); // List with message
        DebugPro.Log(listInts, "Message printed before the list list: ", this);  // List with message and referencing the component's gameObject
    }

}
