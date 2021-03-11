using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Essentials.Examples.DebugEssentials
{
    public class DebugEssentialsExample : MonoBehaviour
    {
    
        void Start()
        {
            UnityEngine.DebugEssentials.Log(" ======== DEBUGGING ARRAY: ======== ");
            int[] arrayInts = {1, 2, 3, 4, 5, 6, 7, 8};
            UnityEngine.DebugEssentials.LogEnumerable(arrayInts); // Array with no message and default separator
            UnityEngine.DebugEssentials.LogEnumerable(arrayInts, " | "); // Array with no message
            UnityEngine.DebugEssentials.LogEnumerable(arrayInts, " - ", "Message printed before the list array: "); // Array with message
            UnityEngine.DebugEssentials.LogEnumerable(arrayInts, " / ", "Message printed before the list array: ", this);  // Array with message and referencing the component's gameObject
        

            UnityEngine.DebugEssentials.Log(" ======== DEBUGGING LIST: ======== ");
            List<int> listInts = arrayInts.ToList();
            UnityEngine.DebugEssentials.LogEnumerable(listInts); // Array with no message and default separator
            UnityEngine.DebugEssentials.LogEnumerable(listInts, " | "); // List with no message
            UnityEngine.DebugEssentials.LogEnumerable(listInts, " - ", "Message printed before the list list: "); // List with message
            UnityEngine.DebugEssentials.LogEnumerable(listInts, " / ", "Message printed before the list list: ", this);  // List with message and referencing the component's gameObject
        }

    }
}
