using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DynamicWeaponData
{
    public bool isUnlocked;

    [Range(0,3)]
    public int rangeUpgrade;
    [Range(0,3)]
    public int damageUpgrade;
    [Range(0,3)]
    public int fireRateUpgrade;
    [Range(0,3)]
    public int ammoUpgrade;

    public DynamicWeaponData()
    {
        isUnlocked = false;
    }
}
