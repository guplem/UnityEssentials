using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Essentials;

namespace Essentials.Examples.Debug_Essentials
{
    public class DebugEssentialsExample : MonoBehaviour
    {
    
        void Start()
        {
            DebugEssentials.Log(" ======== DEBUGGING ARRAY: ======== ");
            int[] arrayInts = {1, 2, 3, 4, 5, 6, 7, 8};
            DebugEssentials.LogEnumerable(arrayInts); // Array with no message and default separator
            DebugEssentials.LogEnumerable(arrayInts, " | "); // Array with no message
            DebugEssentials.LogEnumerable(arrayInts, " - ", "Message printed before the list array: "); // Array with message
            DebugEssentials.LogEnumerable(arrayInts, " / ", "Message printed before the list array: ", this);  // Array with message and referencing the component's gameObject
        

            DebugEssentials.Log(" ======== DEBUGGING LIST: ======== ");
            List<int> listInts = arrayInts.ToList();
            DebugEssentials.LogEnumerable(listInts); // List with no message and default separator
            DebugEssentials.LogEnumerable(listInts, " | "); // List with no message
            DebugEssentials.LogEnumerable(listInts, " - ", "Message printed before the list list: "); // List with message
            DebugEssentials.LogEnumerable(listInts, " / ", "Message printed before the list list: ", this);  // List with message and referencing the component's gameObject
        }

    }
}
