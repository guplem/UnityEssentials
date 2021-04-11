using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Essentials.Examples.Animations.CameraAnimation
{
    public class ColorAnimationExample : MonoBehaviour
    {
        [SerializeField] private Image imageToChangeColor;
        private SimpleAnimationsManager simpleAnimationsManager;

        private void ApplyColorToImage()
        {
            ColorAnimation colorAnimation = ((ColorAnimation) simpleAnimationsManager.GetAnimation("Color"));
            imageToChangeColor.color = colorAnimation.colorToAnimate;
        }

        void Start()
        {
            simpleAnimationsManager = GetComponent<SimpleAnimationsManager>();
            Debug.Log("Press H to play and Y to stop the animation inserted trough the inspector");
        }
    
        void Update()
        {
        
            // PLAY
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Playing an animation configured trough the inspector.");
                simpleAnimationsManager.Play(0);
            }
        
            // STOP
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("Stopping an animation configured trough the inspector.");
                simpleAnimationsManager.Stop(0);
            }
        
        
        }

    }
}