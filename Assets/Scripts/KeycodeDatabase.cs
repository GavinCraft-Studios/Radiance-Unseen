using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycodeDatabase : MonoBehaviour, IDataPersistance
{
    public Dictionary<int, KeyCode> keycodeDictionary;

    public void LoadGame(GameData data)
    {
        this.keycodeDictionary = data.keycodeDatabase;
    }

    public void SaveGame(GameData data)
    {
        data.keycodeDatabase = this.keycodeDictionary;
    }

    public KeyCode GetKeycodeInDatabase(int intIdenifier = 0)
    {
        KeyCode wantedKey;
        wantedKey = this.keycodeDictionary[intIdenifier];
        return wantedKey;
    }

    public void SetKeycodeInDatabase(int identifier2, KeyCode setKeycode)
    {
        if (keycodeDictionary.ContainsKey(identifier2))
        {
            this.keycodeDictionary[identifier2] = setKeycode;
        }
        else
        {
            Debug.LogWarning("Keycode identifier not recogized: " + identifier2 + " while trying to save keycode: " + setKeycode.ToString());
        }
    }

    public Dictionary<int, KeyCode> GetFullDictionary()
    {
        return this.keycodeDictionary;
    }
}
