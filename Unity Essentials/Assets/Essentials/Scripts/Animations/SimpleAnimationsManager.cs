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
        /// <summary>
        /// List of all the animations meant to be configured trough the inspector and/or stored in the SimpleAnimationsManager's memory.
        /// <para>This animations can be played/stopped using the methods that require an index instead of a SimpleAnimation object.</para>
        /// </summary>
        [SerializeReference] public List<ISimpleAnimation> animations;
        
        private HashSet<SimpleAnimation> playingAnimations;

        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="animation">The animation that is wanted to be played.</param>
        /// <param name="resume">If the animation should continue where it was left (true) or restart (false, default).</param>
        public void Play(SimpleAnimation animation, bool resume = false)
        {
            if (playingAnimations == null)
                playingAnimations = new HashSet<SimpleAnimation>();

            if (playingAnimations.Add(animation))
                if (!resume)
                    animation.Reset();
        }
        
        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="index">The index of animation in the 'SimpleAnimationsManager' that is wanted to be played.</param>
        /// <param name="resume">If the animation should continue where it was left (true) or restart (false, default).</param>
        public void Play(int index, bool resume = false)
        {
            if (animations.Count > index)
                Play((SimpleAnimation)animations[index], resume);
            else
                Debug.LogWarning("Trying to play a non-existing animation in the SimpleAnimationsManager of the GameObject " + gameObject.name, gameObject);
        }
        
        /// <summary>
        /// Stops playing the animation.
        /// </summary>
        /// <param name="animation">The animation that is wanted to be stopped.</param>
        public void Stop(SimpleAnimation animation)
        {
            if (playingAnimations != null)
                playingAnimations.Remove(animation);
        }
        
        /// <summary>
        /// Stops playing the animation.
        /// </summary>
        /// <param name="index">The index of animation in the 'SimpleAnimationsManager' that is wanted to be stopped.</param>
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