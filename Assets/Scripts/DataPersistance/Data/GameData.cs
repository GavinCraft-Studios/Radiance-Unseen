using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // most recentley used save
    public long lastUpdated;

    // settings data, (keycode starts at 0)
    public bool fullscreen;

    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;
    public float VoiceLVolume;

    public Dictionary<int, KeyCode> keycodeDatabase;

    
    // playtime + save display data
    public float playtimeSec;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.fullscreen = true;
        keycodeDatabase = new Dictionary<int, KeyCode>(){
            // Move Up
            {0, KeyCode.W},
            // Move Down
            {1, KeyCode.S},
            // Move Left
            {2, KeyCode.A},
            // Move Right
            {3, KeyCode.D},
            // Main Weapon Select
            {4, KeyCode.Alpha1},
            // Sub Weapon Select
            {5, KeyCode.Alpha2},
            // Grenade Select
            {6, KeyCode.Alpha3},
            // Weapon Interact
            {7, KeyCode.Q},
            // Power Cell Select
            {8, KeyCode.Alpha4}
        };
    }
}
