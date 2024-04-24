using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour, IDataPersistance
{
    [Header("Config")]
    public bool saveData;

    [Header("Main Weapons")]
    public WeaponData BK27;
    public WeaponData BK28;
    public WeaponData RAN38;
    public WeaponData RAN56;
    public WeaponData SAW249;
    public WeaponData BR23;

    [Header("Sub Weapons")]
    public WeaponData P88;
    public WeaponData P129;
    public WeaponData SAW541;
    public WeaponData BARB;
    public WeaponData ST25;
    public WeaponData ST78;

    public void LoadGame(GameData data)
    {
        if (saveData) {
            BK27.dynamicData = data.BK27;
            BK28.dynamicData = data.BK28;
            RAN38.dynamicData = data.RAN38;
            RAN56.dynamicData = data.RAN56;
            SAW249.dynamicData = data.SAW249;
            BR23.dynamicData = data.BR23;

            P88.dynamicData = data.P88;
            P129.dynamicData = data.P129;
            SAW541.dynamicData = data.SAW541;
            BARB.dynamicData = data.BARB;
            ST25.dynamicData = data.ST25;
            ST78.dynamicData = data.ST78;
        }
    }

    public void SaveGame(GameData data)
    {
        if (saveData) {
            data.BK27 = BK27.dynamicData;
            data.BK28 = BK28.dynamicData;
            data.RAN38 = RAN38.dynamicData;
            data.RAN56 = RAN56.dynamicData;
            data.SAW249 = SAW249.dynamicData;
            data.BR23 = BR23.dynamicData;

            data.P88 = P88.dynamicData;
            data.P129 = P129.dynamicData;
            data.SAW541 = SAW541.dynamicData;
            data.BARB = BARB.dynamicData;
            data.ST25 = ST25.dynamicData;
            data.ST78 = ST78.dynamicData;
        }
    }

    public List<WeaponData> getWeaponList()
    {
        List<WeaponData> weaponList = new List<WeaponData> {
            BK27, BK28, RAN38, RAN56, SAW249, BR23, P88, P129, SAW541, BARB, ST25, ST78 };
        return weaponList;
    }
}