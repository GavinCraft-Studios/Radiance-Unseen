using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // most recentley used save (used for continue game)
    public long lastUpdated;

    // playtime + save display data
    public float playtimeSec;

    // settings data, (keycode starts at 0)
    public bool fullscreen;

    public float masterVolume;
    public float ambienceVolume;
    public float musicVolume;
    public float sfxVolume;
    public float voiceVolume;

    public Dictionary<int, KeyCode> keycodeDatabase;

    // dynamic weapon data
    public DynamicWeaponData BK27;
    public DynamicWeaponData BK28;
    public DynamicWeaponData RAN38;
    public DynamicWeaponData RAN56;
    public DynamicWeaponData SAW249;
    public DynamicWeaponData BR23;

    public DynamicWeaponData P88;
    public DynamicWeaponData P129;
    public DynamicWeaponData SAW541;
    public DynamicWeaponData BARB;
    public DynamicWeaponData ST25;
    public DynamicWeaponData ST78;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        // Settings
        this.fullscreen = true;

        masterVolume = 1f;
        ambienceVolume = 1f;
        musicVolume = 1f;
        sfxVolume = 1f;
        voiceVolume = 1f;

        // KeyCodes
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
            {8, KeyCode.Alpha4},
            // Pause Menu
            {9, KeyCode.Escape}
        };

        BK27 = new DynamicWeaponData();
        BK28 = new DynamicWeaponData();
        RAN38 = new DynamicWeaponData();
        RAN56 = new DynamicWeaponData();
        SAW249 = new DynamicWeaponData();
        BR23 = new DynamicWeaponData();

        P88 = new DynamicWeaponData();
        P129 = new DynamicWeaponData();
        SAW541 = new DynamicWeaponData();
        BARB = new DynamicWeaponData();
        ST25 = new DynamicWeaponData();
        ST78 = new DynamicWeaponData();        
    }
}