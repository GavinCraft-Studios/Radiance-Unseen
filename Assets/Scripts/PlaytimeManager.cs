using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaytimeManager : MonoBehaviour, IDataPersistance
{
    // Setup
    private float oldPlaytimeSec;

    // loading previous save data time
    public void LoadGame(GameData data)
    {
        this.oldPlaytimeSec = data.playtimeSec;
        secsInScene = 0;
        stopwatchActive = true;
    }

    public void SaveGame(GameData data)
    {
        stopwatchActive = false;
        data.playtimeSec += this.secsInScene;
    }

    // Scene Playtime Stopwatch
    [Header("Current Session Playtime:")]
    [SerializeField] private string playtimeDisplay;
    [Header("Debugging")]
    [SerializeField] private bool stopwatchActive = false;
    [SerializeField] private float secsInScene;

    void Update()
    {
        if (stopwatchActive)
        {
            secsInScene = secsInScene + Time.unscaledDeltaTime;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(secsInScene);
        playtimeDisplay = "Playtime (H:M): " + timeSpan.Hours.ToString() + ":" + timeSpan.Minutes.ToString();
    }

}
