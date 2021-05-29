using System;
using System.Collections.Generic;

namespace UnityEngine
{
    /// <summary>
    /// Component used to control the behaviour of any SimpleAnimation
    /// </summary>
    public class SimpleAnimationsManager : MonoBehaviour
    {
        /// <summary>
        /// List of all the animations meant to be configured trough the inspector and/or stored in the SimpleAnimationsManager's memory.
        /// <para>This animations can be played/stopped using the methods that require an index instead of a SimpleAnimation object.</para>
        /// </summary>
        [SerializeReference] public List<ISimpleAnimation> animations;
        /// <summary>
        /// Obtain an animation configured trough the inspector and stored in the SimpleAnimationsManager's memory.
        /// <para>Index of the animation in the inspector if the SimpleAnimationsManager component.</para>
        /// </summary>
        public SimpleAnimation GetAnimation(int index)
        {
            if (animations.Count > index)
                return (SimpleAnimation) animations[index];
            
            Debug.LogWarning($"Trying to get the animation with index '{index}' of the 'SimpleAnimationsManager' of the GameObject {gameObject.name} but the size of the array is {animations.Count}.", gameObject);
            return null;
        }
        /// <summary>
        /// Returns the simple animation stored in this SimpleAnimationsManager witht he same name as the given. 
        /// </summary>
        /// <param name="animationName">The name of the wanted animation.</param>
        /// <returns></returns>
        public SimpleAnimation GetAnimation(string animationName)
        {
            foreach (ISimpleAnimation IAnimation in animations)
            {
                SimpleAnimation animation = (SimpleAnimation)IAnimation;
                if (string.Compare(animation.name, animationName, StringComparison.Ordinal) == 0)
                    return animation;
            }
            Debug.LogWarning($"Trying to find the animation with name '{animationName}' but it is not found in the 'SimpleAnimationsManager' of the GameObject {gameObject.name}", gameObject);
            return null;
        }
        
        /// <summary>
        /// List of all the animations that should stopped so they will be removed from the "playingAnimations" list at the next frame.
        /// </summary>
        private HashSet<SimpleAnimation> animationsToStop = new HashSet<SimpleAnimation>();
        
        /// <summary>
        /// List of all the animations that are being played.
        /// </summary>
        private HashSet<SimpleAnimation> playingAnimations = new HashSet<SimpleAnimation>();

        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="animation">The animation that is wanted to be played.</param>
        /// <param name="resume">If the animation should continue where it was left (true) or restart (false, default).</param>
        public void Play(SimpleAnimation animation, bool resume = false)
        {
            playingAnimations.Add(animation);

            if (!resume)
                animation.Reset();
            
            animationsToStop.Remove(animation);
        }
        
        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="animationName">The name of the animation in the 'SimpleAnimationsManager' that is wanted to be played.</param>
        /// <param name="resume">If the animation should continue where it was left (true) or restart (false, default).</param>
        public void Play(string animationName, bool resume = false)
        {
            Play(GetAnimation(animationName), resume);
        }

        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="index">The index of animation in the 'SimpleAnimationsManager' that is wanted to be played.</param>
        /// <param name="resume">If the animation should continue where it was left (true) or restart (false, default).</param>
        public void Play(int index, bool resume = false)
        {
            Play(GetAnimation(index), resume);
        }
        
        /// <summary>
        /// Stops playing the animation.
        /// </summary>
        /// <param name="animation">The animation that is wanted to be stopped.</param>
        public void Stop(SimpleAnimation animation)
        {
            playingAnimations.Remove(animation);
            animationsToStop.Remove(animation);
        }
        
        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="animationName">The name of the animation in the 'SimpleAnimationsManager' that is wanted to be played.</param>
        public void Play(string animationName)
        {
            Play(GetAnimation(animationName));
        }
        
        /// <summary>
        /// Stops playing the animation.
        /// </summary>
        /// <param name="index">The index of the animation in the 'SimpleAnimationsManager' that is wanted to be stopped.</param>
        public void Stop(int index)
        {
            Stop(GetAnimation(index));
        }
        
        /// <summary>
        /// Stops playing the animation.
        /// </summary>
        /// <param name="animationName">The name of the animation in the 'SimpleAnimationsManager' that is wanted to be stopped.</param>
        public void Stop(string animationName)
        {
            Stop(GetAnimation(animationName));
        }

        private void Update()
        {
            if (animationsToStop.Count > 0)
            {
                List<SimpleAnimation> tempAnimToStop = new List<SimpleAnimation>(animationsToStop);
                foreach (SimpleAnimation animation in tempAnimToStop)
                {
                    Stop(animation);
                }
            }
            
            
            foreach (SimpleAnimation animation in playingAnimations)
            {
                if (animation.Step(Time.deltaTime))
                {
                    animationsToStop.Add(animation);
                }
            }
        }
        
        /// <summary>
        /// Sets the animation at the given progress.
        /// </summary>
        /// <param name="animation">The animation in the 'SimpleAnimationsManager' that is wanted to be updated with the new progress.</param>
        /// <param name="progress">The progress of the animation [0,1]</param>
        public void SetProgress(SimpleAnimation animation, float progress)
        {
            animation.SetProgress(progress);
        }
        
        /// <summary>
        /// Sets the animation at the given progress.
        /// </summary>
        /// <param name="animationName">The name of the animation in the 'SimpleAnimationsManager' that is wanted to be updated with the new progress.</param>
        /// <param name="progress">The progress of the animation [0,1]</param>
        public void SetProgress(string animationName, float progress)
        {
            SetProgress(GetAnimation(animationName), progress);
        }
        
        /// <summary>
        /// Sets the animation at the given progress.
        /// </summary>
        /// <param name="index">The index of animation in the 'SimpleAnimationsManager' that is wanted to be updated with the new progress.</param>
        /// <param name="progress">The progress of the animation [0,1]</param>
        public void SetProgress(int index, float progress)
        {
            SetProgress(GetAnimation(index), progress);
        }
    }
    
}