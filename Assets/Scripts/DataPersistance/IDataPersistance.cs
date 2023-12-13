using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    void LoadGame(GameData data);
    void SaveGame(GameData data);
}
