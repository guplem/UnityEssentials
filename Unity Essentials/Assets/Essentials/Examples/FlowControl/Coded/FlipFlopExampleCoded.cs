using UnityEngine;

namespace Essentials.Examples.FlowControl
{
    public class FlipFlopExampleCoded : MonoBehaviour
    {
        private FlipFlop flipFlop;

        private void Start()
        {
            flipFlop = new FlipFlop(DummyMethodA, DummyMethodB);
            
            Debug.Log("Press 'F' to invoke the FlipFlop.");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flipFlop.Invoke();
            }
        }

        public void DummyMethodA()
        {
            Debug.Log("Executing method from FlipFlop (A)");
        }
        
        public void DummyMethodB()
        {
            Debug.Log("Executing method from FlipFlop (B)");
        }
    }
}
