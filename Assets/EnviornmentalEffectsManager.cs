using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Audio;

public class EnviornmentalEffectsManager : MonoBehaviour
{
    [Header("Enviornmental Music")]
    public bool isMusicActive = true;
    public AudioClip music;
    public float volume = 1f;
    public float pitch = 1f;
    public AudioClip pauseMusic;

    private PauseOS_Controller pauseOS_Controller;
    private AudioSource audioSource;
    private AudioSource osAudioSource;

    void Start()
    {
        // Envoirnmental Music
        pauseOS_Controller = GameObject.Find("Pause OS").GetComponent<PauseOS_Controller>();
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();
        osAudioSource = this.gameObject.AddComponent<AudioSource>();
        osAudioSource.clip = pauseMusic;
    }

    void Update()
    {
        UpdateMusic();
    }

    public void UpdateMusic()
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        if (pauseOS_Controller.isFullyOpen & !osAudioSource.isPlaying)
        {
            audioSource.Pause();
            osAudioSource.Play();
        }
        else if (osAudioSource.isPlaying)
        {
            audioSource.Play();
            osAudioSource.Stop();
        }
    }
}
