using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> audioClips;

    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    
    public float getRandomPitch(float minInclusive, float maxInclusive)
    {
        return Random.Range(minInclusive, maxInclusive);
    }

    // Play Audio From List:
    public void PlayAudioFromList(int index, float volume, float pitch)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    public void PlayAudioFromList(int index, float volume)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];
        audioSource.volume = volume;
        audioSource.pitch = 1;
        audioSource.Play();
    }

    public void PlayAudioFromList(int index)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[index];
        audioSource.volume = 1;
        audioSource.pitch = 1;
        audioSource.Play();
    }

    // Play Audio
    public void PlayAudio(AudioClip clip, float volume, float pitch)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    public void PlayAudio(AudioClip clip, float volume)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = 1;
        audioSource.Play();
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = 1;
        audioSource.pitch = 1;
        audioSource.Play();
    }
}
