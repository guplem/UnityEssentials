using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace UnityEngine
{
    public class AudioSourceManager : MonoBehaviour
    {
        [Tooltip("The amount of Audio Sources that will exist in the GameObject where this component lives. If they are missing, they will be created during this component's Awake.")]
        [SerializeField] private int minimumQuantityOfExistingAudioSources;
        [HideInInspector] private List<AudioSource> audioSources;


        /// <summary>
        /// Plays the clip.
        /// </summary>
        /// <param name="clip">The AudioClip to play</param>
        /// <param name="volume">The volume of the AudioSource playing the clip (0.0 to 1.0)</param>
        /// <param name="loop">Loop the AudioClip?</param>
        /// <param name="pitch">Pitch shift of the AudioSource</param>
        /// <param name="delay">The volume of the AudioSource (0.0 to 1.0)</param>
        /// <param name="audioMixerGroup">The target group to which the AudioSource should route its signal</param>
        /// <returns></returns>
        public AudioSource PlayClip(AudioClip clip, float volume = 1, bool loop = false, float pitch = 1, float delay = 0f, AudioMixerGroup audioMixerGroup = null)
        {
            if (clip == null)
            {
                Debug.LogWarning("Trying to play a null clip", this);
                return null;
            }

            AudioSource configuredAudioSource = ConfigureAudioSource(GetFreeAudioSource(), clip, volume, loop, pitch, audioMixerGroup);
            configuredAudioSource.PlayDelayed(delay);
            return configuredAudioSource;
        }
        
        public AudioSource StopClip(AudioClip clipInAudioSource)
        {
            AudioSource audioSourceWithClip = GetAudioSource(clipInAudioSource);
            audioSourceWithClip.Stop();
            return audioSourceWithClip;
        }

        public AudioSource GetAudioSource(int index)
        {
            return audioSources[index];
        }
        
        public void StopAllClips(bool fadeOut = false)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (fadeOut)
                {
                    //ToDo: Be able to stop the sounds with any type of animation curve
                    FadeOutAudioSource(audioSource);
                }
                else
                {
                    audioSource.Stop();
                }
            }
        }

        public AudioSource GetAudioSource(AudioClip clipInAudioSource)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.clip == clipInAudioSource)
                {
                    return audioSource;
                }
            }
            
            Debug.LogWarning($"Audio Source with clip '{clipInAudioSource.name}' not found ");
            return null;
        }
        
        public List<AudioSource> GetAudioSources(AudioClip clipInAudioSource)
        {
            List<AudioSource> foundAudioSources = new List<AudioSource>();
            foreach (AudioSource audioSource in this.audioSources)
            {
                if (audioSource.clip == clipInAudioSource)
                {
                    foundAudioSources.Add(audioSource);
                }
            }
            
            if (foundAudioSources.Count <= 0)
                Debug.LogWarning($"Audio Sources with clip '{clipInAudioSource.name}' not found ");
            
            return foundAudioSources;
        }

        private void Awake()
        {
            audioSources = new List<AudioSource>();

            foreach (AudioSource audioSource in GetComponents<AudioSource>())
            {
                audioSources.Add(audioSource);
                audioSource.enabled = true;
            }

            for (int i = audioSources.Count; i < minimumQuantityOfExistingAudioSources; i++)
                AddAudioSource();
        }

        private AudioSource GetFreeAudioSource()
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }

            return AddAudioSource();
        }

        // ToDo: check if this is the way is wanted to be the default configuration
        private AudioSource AddAudioSource()
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;
            //newAudioSource.spatialBlend = 1f;
            //newAudioSource.rolloffMode = AudioRolloffMode.Linear;
            //newAudioSource.minDistance = 0.0f;
            //newAudioSource.maxDistance = 30.0f; 
            audioSources.Add(newAudioSource);

            return newAudioSource;
        }

        private AudioSource ConfigureAudioSource(AudioSource audioSource, AudioClip clip, float volume = 1, bool loop = false, float pitch = 1, AudioMixerGroup audioMixerGroup = null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.pitch = pitch;
            audioSource.loop = loop;

            return audioSource;
        }
        
        private void FadeOutAudioSource(AudioSource audioSource)
        {
            StartCoroutine(LowerVolumeAndStopSounds(audioSource));
        }

        private static IEnumerator LowerVolumeAndStopSounds(AudioSource audioSource)
        {
            while (audioSource.volume > 0f)
            {
                audioSource.volume -= 0.1f;
                yield return new WaitForSeconds(0.12f);

            }
            audioSource.Stop();
            audioSource.volume = 1.0f;
        }
    }
}
