using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace UnityEngine
{
    /// <summary>
    /// Component to manage multiple AudioSources (or AudioClips) at the same time (or in the same component)
    /// </summary>
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
        
        /// <summary>
        /// Stops playing the clip in all AudioSources related to this AudioSoundManager that are playing it.
        /// </summary>
        /// <param name="clipInAudioSource">The AudioClip that must stop playing.</param>
        /// <returns></returns>
        public List<AudioSource> StopClip(AudioClip clipInAudioSource)
        {
            List<AudioSource> audioSourcesWithClip = GetAudioSources(clipInAudioSource);
            foreach (AudioSource audioSourceWithClip in audioSourcesWithClip)
            {
                audioSourceWithClip.Stop();
            }

            return audioSourcesWithClip;
        }

        /// <summary>
        /// Returns the AudioSource with the given index from the list of managed audio sources of this AudioSourceManager.
        /// </summary>
        /// <param name="index">The index of the AudioSource</param>
        /// <returns></returns>
        public AudioSource GetAudioSource(int index)
        {
            return audioSources[index];
        }
        
        /// <summary>
        /// Stops playing all clips i all AudioSources related to this AudioSourceManager.
        /// </summary>
        /// <param name="fadeOut"></param>
        public void StopAllClips(bool fadeOut = false)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (fadeOut)
                {
                    FadeOutAudioSource(audioSource);
                }
                else
                {
                    audioSource.Stop();
                }
            }
        }

        /// <summary>
        /// Returns an AudioSource that is playing the given clip.
        /// </summary>
        /// <param name="clipInAudioSource">The clip that must be playing the returned AudioSource.</param>
        /// <returns></returns>
        public AudioSource GetAudioSource(AudioClip clipInAudioSource)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.clip == clipInAudioSource)
                {
                    if (audioSource.isPlaying)
                        return audioSource;
                }
            }
            
            //Debug.LogWarning($"No Audio Source playing the clip '{clipInAudioSource.name}' was found ");
            return null;
        }
        
        /// <summary>
        /// Returns all AudioSources that are playing the given clip.
        /// </summary>
        /// <param name="clipInAudioSource">The clip that all returned AudioSources must be playing.</param>
        /// <returns></returns>
        public List<AudioSource> GetAudioSources(AudioClip clipInAudioSource)
        {
            List<AudioSource> foundAudioSources = new List<AudioSource>();
            foreach (AudioSource audioSource in this.audioSources)
            {
                if (audioSource.clip == clipInAudioSource)
                {
                    if (audioSource.isPlaying)
                        foundAudioSources.Add(audioSource);
                }
            }
            
            //if (foundAudioSources.Count <= 0)
            //    Debug.LogWarning($"No Audio Sources playing the clip '{clipInAudioSource.name}' were found ");
            
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
        
        private AudioSource AddAudioSource()
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;
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
