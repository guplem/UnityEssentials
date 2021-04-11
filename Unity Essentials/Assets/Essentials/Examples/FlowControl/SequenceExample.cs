using System;
using UnityEngine;
using UnityEngine.UI;

namespace Essentials.Examples.FlowControl
{
    public class SequenceExample : MonoBehaviour
    {
        [SerializeField] private Sequence sequence;

        private void Start()
        {
            Debug.Log("Press 'S' to invoke the sequence.");
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
            Debug.Log("A");
        }
        
        public void DummyMethodB()
        {
            Debug.Log("B");
        }
        
        public void DummyMethodC()
        {
            Debug.Log("C");
        }
    }
}
