using UnityEngine;

namespace Essentials.Examples.FlowControl
{
    public class DoOnceExampleCoded : MonoBehaviour
    {
        private DoOnce doOnce;

        private void Start()
        {
            doOnce = new DoOnce(DummyMethod);
            
            Debug.Log("Press 'O' to invoke the DoOnce.");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                doOnce.Invoke();
            }
        }

        public void DummyMethod()
        {
            Debug.Log("Executing DummyMethod of 'DoOnce'");
        }
        
    }
}
