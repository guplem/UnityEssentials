using System.Collections.Generic;

namespace UnityEngine
{

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
            return (SimpleAnimation) animations[index];
        }
        
        // List of all the animations that should stopped so they will be removed from the "playingAnimations" list at the next frame.
        private HashSet<SimpleAnimation> animationsToStop = new HashSet<SimpleAnimation>();
        
        // List of all the animations that are being played.
        private HashSet<SimpleAnimation> playingAnimations = new HashSet<SimpleAnimation>();

        /// <summary>
        /// Starts playing the animation.
        /// </summary>
        /// <param name="animation">The animation that is wanted to be played.</param>
        /// <param name="resume">If the animation should continue where it was left (true) or restart (false, default).</param>
        public void Play(SimpleAnimation animation, bool resume = false)
        {
            playingAnimations.Add(animation);

            if (!resume) //TODO: If resume is true but the animation already finished, restart (or add another parameter "restartAtFinish" in the SimpleAnimation class?)
                animation.Reset();
            
            animationsToStop.Remove(animation);
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
            playingAnimations.Remove(animation);
            animationsToStop.Remove(animation);
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
    }
    
}