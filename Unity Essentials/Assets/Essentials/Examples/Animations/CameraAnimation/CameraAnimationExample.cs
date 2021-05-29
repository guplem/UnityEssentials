using UnityEngine;

namespace Essentials.Examples.Animations.CameraAnimation
{
    public class CameraAnimationExample : MonoBehaviour
    {

        [SerializeField] private SimpleAnimationsManager simpleAnimationsManager;
        [Space]
        [SerializeField] private Camera cameraToAnimate;
        [SerializeField] private Camera origin;
        [SerializeField] private Camera destination;


        private UnityEngine.CameraAnimation codeAnimation = null;
    
        void Start()
        {
            codeAnimation = new UnityEngine.CameraAnimation(cameraToAnimate, destination, origin, 1f, Curve.Linear);
            //codeAnimation.mirror = true; // If it is wanted to mirror it, you can do so using the 'mirror' variable of the animation
        
            Debug.Log("Press G to play and T to stop the animation inserted by code");
            Debug.Log("Press H to play and Y to stop the animation inserted trough the inspector");
        }
    
        void Update()
        {
        
            // PLAY
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
        
            // STOP
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
    }
}