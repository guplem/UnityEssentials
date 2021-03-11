using UnityEngine;

namespace Essentials.Examples.WarningCS0649
{
    // Dummy class to test if the warning CS0649 appears in the Unity's Console or not
    public class ForceWarningCS0649 : MonoBehaviour
    {
        private GameObject warningVar;

        private void Update()
        {
            warningVar.transform.position = Vector3.zero;
        }
    }
}
