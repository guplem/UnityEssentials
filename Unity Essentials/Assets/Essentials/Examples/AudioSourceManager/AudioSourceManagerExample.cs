using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSourceManager))]
public class AudioSourceManagerExample : MonoBehaviour
{
    private AudioSourceManager audioSourceManager;
    [SerializeField] AudioClip clipA;
    [SerializeField] AudioClip clipB;

    private void Start()
    {
        audioSourceManager = GetComponent<AudioSourceManager>();
    }

    public void PlayClipA()
    {
        audioSourceManager.PlayClip(clipA);
    }
    
    public void PlayClipB()
    {
        audioSourceManager.PlayClip(clipB, 1f, false, 1f, 0f, null);
    }

    public void StopClipB()
    {
        audioSourceManager.StopClip(clipB);
    }

    public void StopAllClips()
    {
        audioSourceManager.StopAllClips();
    }

    public void DecreaseVolumeClipB()
    {
        List<AudioSource> clipAudioSources = audioSourceManager.GetAudioSources(clipB);
        
        foreach (AudioSource clipAudioSource in clipAudioSources)
        {
            float newVolume = Mathf.Clamp01(clipAudioSource.volume - 0.25f);
            clipAudioSource.volume = newVolume;
        }
        
    }
    
    public void IncreaseVolumeClipB()
    {
        List<AudioSource> clipAudioSources = audioSourceManager.GetAudioSources(clipB);
        
        foreach (AudioSource clipAudioSource in clipAudioSources)
        {
            float newVolume = Mathf.Clamp01(clipAudioSource.volume + 0.25f);
            clipAudioSource.volume = newVolume;
        }
    }
    
}
