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
        DebugPro.LogEnumerable(arrayInts); // Array with no message and default separator
        DebugPro.LogEnumerable(arrayInts, " | "); // Array with no message
        DebugPro.LogEnumerable(arrayInts, " - ", "Message printed before the list array: "); // Array with message
        DebugPro.LogEnumerable(arrayInts, " / ", "Message printed before the list array: ", this);  // Array with message and referencing the component's gameObject
        

        DebugPro.Log(" ======== DEBUGGING LIST: ======== ");
        List<int> listInts = arrayInts.ToList();
        DebugPro.LogEnumerable(listInts); // Array with no message and default separator
        DebugPro.LogEnumerable(listInts, " | "); // List with no message
        DebugPro.LogEnumerable(listInts, " - ", "Message printed before the list list: "); // List with message
        DebugPro.LogEnumerable(listInts, " / ", "Message printed before the list list: ", this);  // List with message and referencing the component's gameObject
    }

}
