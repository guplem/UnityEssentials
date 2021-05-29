using UnityEngine;

namespace Essentials.Examples.FlowControl
{
    public class DoNExampleCoded : MonoBehaviour
    {
        private DoN doN;

        private void Start()
        {
            doN = new DoN(DummyMethod, 4);
            
            Debug.Log("Press 'N' to invoke the DoN.");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                doN.Invoke();
            }
        }

        public void DummyMethod()
        {
            Debug.Log($"Executing DummyMethod of 'DoN'. Execution number {doN.invokedTimes}");
        }
        
    }
}
