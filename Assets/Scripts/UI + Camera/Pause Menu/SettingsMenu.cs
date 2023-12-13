using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour, IDataPersistance
{
    [Header("Volume")]
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Slider voiceSlider;

    public float masterData;
    public float musicData;
    public float SFXData;
    public float voiceLData;

    // Data Persistance
    public void LoadGame(GameData data)
    {
        masterVolume(data.masterVolume);
        masterSlider.value = data.masterVolume;
        musicVolume(data.musicVolume);
        musicSlider.value = data.musicVolume;
        SFXVolume(data.SFXVolume);
        SFXSlider.value = data.SFXVolume;
        voiceLVolume(data.VoiceLVolume);
        voiceSlider.value = data.VoiceLVolume;
    }

    public void SaveGame(GameData data)
    {
        data.masterVolume = masterData;
        data.musicVolume = musicData;
        data.SFXVolume = SFXData;
        data.VoiceLVolume = voiceLData;
    }

    // Volumes
    public void masterVolume (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        masterData = volume;
    }

    public void musicVolume (float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        musicData = volume;
    }

    public void SFXVolume (float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SFXData = volume;
    }

    public void voiceLVolume (float volume)
    {
        audioMixer.SetFloat("VoiceLVolume", volume);
        voiceLData = volume;
    }
}
