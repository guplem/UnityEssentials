using UnityEngine;

namespace Essentials.Examples.FlowControl
{
    public class SequenceExample : MonoBehaviour
    {
        [SerializeField] private Sequence sequence;

        private void Start()
        {
            Debug.Log("Press 'S' to invoke the Sequence.");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                sequence.Invoke();
            }
        }

        public void DummyMethodA()
        {
            Debug.Log("Executing method from Sequence (A)");
        }
        
        public void DummyMethodB()
        {
            Debug.Log("Executing method from Sequence (B)");
        }
        
        public void DummyMethodC()
        {
            Debug.Log("Executing method from Sequence (C)");
        }
    }
}
