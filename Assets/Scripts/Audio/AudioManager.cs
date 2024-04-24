using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using JetBrains.Annotations;
using System.Dynamic;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour, IDataPersistance
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float ambienceVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;
    [Range(0, 1)]
    public float voiceVolume = 1;

    private Bus masterBus;
    private Bus ambienceBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus voiceBus;

    [Header("Scrollbar Refs.")]
    public Scrollbar masterScroll;
    //private Scrollbar ambienceScroll;
    public Scrollbar musicScroll;
    public Scrollbar sfxScroll;
    public Scrollbar voiceScroll;

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        voiceBus = RuntimeManager.GetBus("bus:/Voice");
    }

    private void Start()
    {
        if (FMODEvents.instance.useAmbience) {InitializeAmbience(FMODEvents.instance.ambience);}
        InitializeMusic(FMODEvents.instance.music);
    }

    // Save & Load:
    public void LoadGame(GameData data) 
    {
        masterScroll.value = data.masterVolume;
        //ambienceScroll.value = data.ambienceVolume;
        musicScroll.value = data.musicVolume;
        sfxScroll.value = data.sfxVolume;
        voiceScroll.value = data.voiceVolume;    
    }

    public void SaveGame(GameData data)
    {
        data.masterVolume = this.masterVolume;
        //data.ambienceVolume = this.ambienceVolume;
        data.musicVolume = this.musicVolume;
        data.sfxVolume = this.sfxVolume;
        data.voiceVolume = this.voiceVolume;
    }

    // ---------------------------------

    private void Update()
    {
        this.masterVolume = masterScroll.value;
        //ambienceVolume = ambienceScroll.value;
        this.musicVolume = musicScroll.value;
        this.sfxVolume = sfxScroll.value;
        voiceVolume = voiceScroll.value; 

        this.masterBus.setVolume(masterVolume);
        //ambienceBus.setVolume(ambienceVolume);
        this.musicBus.setVolume(musicVolume);
        this.sfxBus.setVolume(sfxVolume);
        voiceBus.setVolume(voiceVolume);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = FMODUnity.RuntimeManager.CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = FMODUnity.RuntimeManager.CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void SetMusicTrackByIndex(int musicTrack)
    {
        musicEventInstance.setParameterByName("musicTrack", (float)musicTrack);
    }

    public void SetMusicTrackByName(string trackName)
    {
        try
        {
            int musicTrack = FMODEvents.instance.tracks.IndexOf(trackName);
            musicEventInstance.setParameterByName("musicTrack", (float)musicTrack);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        // stop all of the event emitters, because if we do0n't they may hang around in other scenes
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
        //set track to default
        SetMusicTrackByIndex(0);
    }
}
