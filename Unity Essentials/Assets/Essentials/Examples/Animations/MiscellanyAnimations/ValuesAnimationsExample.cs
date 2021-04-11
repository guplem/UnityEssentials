using UnityEngine;
using UnityEngine.UI;

namespace Essentials.Examples.Animations.MiscellanyAnimations
{
    public class ValuesAnimationsExample : MonoBehaviour
    {
        
        private SimpleAnimationsManager simpleAnimationsManager;

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
                simpleAnimationsManager.Play("Float");
                simpleAnimationsManager.Play("Int");
                simpleAnimationsManager.Play("Vector2");
                simpleAnimationsManager.Play("Vector3");
            }
        
            // STOP
            if (Input.GetKeyDown(KeyCode.Y))
            {
                simpleAnimationsManager.Stop("Float");
                simpleAnimationsManager.Stop("Int");
                simpleAnimationsManager.Stop("Vector2");
                simpleAnimationsManager.Stop("Vector3");
            }
        
        }
        
        public void ApplyFloatToText(Text text)
        {
            FloatAnimation floatAnimation = ((FloatAnimation) simpleAnimationsManager.GetAnimation("Float"));
            text.text = floatAnimation.floatToAnimate.ToString();
        }
        
        public void ApplyIntToText(Text text)
        {
            IntAnimation intAnimation = ((IntAnimation) simpleAnimationsManager.GetAnimation("Int"));
            text.text = intAnimation.intToAnimate.ToString();
        }
        
        public void ApplyVector3ToText(Text text)
        {
            Vector3Animation vector3Animation = ((Vector3Animation) simpleAnimationsManager.GetAnimation("Vector3"));
            text.text = vector3Animation.vector3ToAnimate.ToString();
        }
        
        public void ApplyVector2ToText(Text text)
        {
            Vector2Animation vector2Animation = ((Vector2Animation) simpleAnimationsManager.GetAnimation("Vector2"));
            text.text = vector2Animation.vector2ToAnimate.ToString();
        }

    }
}
