using UnityEngine;
using UnityEngine.UI;

namespace Essentials.Examples.Animations.MiscellanyAnimations
{
    public class ColorAnimationExample : MonoBehaviour
    {
        
        private SimpleAnimationsManager simpleAnimationsManager;

        void Start()
        {
            simpleAnimationsManager = GetComponent<SimpleAnimationsManager>();
        }
    
        void Update()
        {
        
            // PLAY
            if (Input.GetKeyDown(KeyCode.H))
            {
                simpleAnimationsManager.Play("Color");
            }
        
            // STOP
            if (Input.GetKeyDown(KeyCode.Y))
            {
                simpleAnimationsManager.Stop("Color");
            }
        
        }
        
        public void ApplyColorToImage(Image imageToChangeColor)
        {
            ColorAnimation colorAnimation = ((ColorAnimation) simpleAnimationsManager.GetAnimation("Color"));
            imageToChangeColor.color = colorAnimation.colorToAnimate;
        }

    }
}