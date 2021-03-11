using UnityEngine;

namespace Essentials.Examples.Animations.RectTransformAnimation
{
    public class RectTransformAnimationExample : MonoBehaviour
    {
        [SerializeField] private SimpleAnimationsManager simpleAnimationsManager;
        [Space]
        [SerializeField] private RectTransform origin;
        [SerializeField] private RectTransform destination;


        private UnityEngine.RectTransformAnimation codeAnimation = null;
    
        void Start()
        {
            codeAnimation = new UnityEngine.RectTransformAnimation(GetComponent<RectTransform>(), destination, origin, 1f, Curve.Linear);
        
            Debug.Log("Press G to play and T to stop the animation inserted by code");
            Debug.Log("Press H to play and Y to stop the animation inserted trough the inspector");
        }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log("Playing an animation configured trough code.");
                simpleAnimationsManager.Play(codeAnimation);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Playing an animation configured trough the inspector.");
                simpleAnimationsManager.Play(0);
            }
        
        
        
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Stopping an animation configured trough code.");
                simpleAnimationsManager.Stop(codeAnimation);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("Stopping an animation configured trough the inspector.");
                simpleAnimationsManager.Stop(0);
            }
        }

        public void InvertAndPlayAgain()
        {
            SimpleAnimation anim = simpleAnimationsManager.GetAnimation(0);
        
            anim.mirror = !anim.mirror;
            simpleAnimationsManager.Play(anim);
        
            // Same result using: 
            // simpleAnimationsManager.Play(0);
        }
    }
}
