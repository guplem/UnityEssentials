using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityEngine
{

    public class SimpleAnimationsManager : MonoBehaviour
    {

        [SerializeReference] public List<ISimpleAnimation> animations;
        
        private HashSet<SimpleAnimation> playingAnimations;


        public void Play(SimpleAnimation animation)
        {
            if (playingAnimations == null)
                playingAnimations = new HashSet<SimpleAnimation>();

            if (playingAnimations.Add(animation))
                animation.Reset();
        }
        
        public void Play(int index)
        {
            if (animations.Count > index)
                Play((SimpleAnimation)animations[index]);
            else
                Debug.LogWarning("Trying to play a non-existing animation in the SimpleAnimationsManager of the GameObject " + gameObject.name, gameObject);
        }

        private void Update()
        {
            if (playingAnimations != null)
            {
                List<SimpleAnimation> animationsToRemove = new List<SimpleAnimation>();
                foreach (SimpleAnimation animation in playingAnimations)
                {
                    if (animation.Step(Time.deltaTime))
                        animationsToRemove.Add(animation);
                }

                foreach (SimpleAnimation animation in animationsToRemove)
                {
                    Debug.Log("FINISHING ANIMATION " + animation);
                    playingAnimations.Remove(animation);
                }
            }

        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}