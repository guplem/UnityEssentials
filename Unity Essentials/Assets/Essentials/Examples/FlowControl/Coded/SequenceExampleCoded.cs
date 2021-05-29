using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Essentials.Examples.FlowControl
{
    public class SequenceExampleCoded : MonoBehaviour
    {
        private Sequence sequence;

        private void Start()
        {
            List<UnityAction> unityActions = new List<UnityAction>();
            unityActions.Add(DummyMethodA);
            unityActions.Add(DummyMethodB);
            unityActions.Add(DummyMethodC);
            sequence = new Sequence(unityActions.ToArray(), true);
                
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
