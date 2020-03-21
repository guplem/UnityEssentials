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


        public void Play(SimpleAnimation animation, bool resume = false)
        {
            if (playingAnimations == null)
                playingAnimations = new HashSet<SimpleAnimation>();

            if (playingAnimations.Add(animation))
                if (!resume)
                    animation.Reset();
        }
        
        public void Play(int index, bool resume = false)
        {
            if (animations.Count > index)
                Play((SimpleAnimation)animations[index], resume);
            else
                Debug.LogWarning("Trying to play a non-existing animation in the SimpleAnimationsManager of the GameObject " + gameObject.name, gameObject);
        }
        
        public void Stop(SimpleAnimation animation)
        {
            if (playingAnimations != null)
                playingAnimations.Remove(animation);
        }
        
        public void Stop(int index)
        {
            if (animations.Count > index)
                Stop((SimpleAnimation)animations[index]);
            else
                Debug.LogWarning("Trying to stop a non-existing animation in the SimpleAnimationsManager of the GameObject " + gameObject.name, gameObject);
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
                    playingAnimations.Remove(animation);
                }
            }

        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}