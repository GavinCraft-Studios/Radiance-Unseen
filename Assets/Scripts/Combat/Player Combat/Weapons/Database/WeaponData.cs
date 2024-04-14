using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : MonoBehaviour
{
    [Header("Constants")]
    public string displayName;
    //public string identifier;
    public Sprite sprite;

    [Range(1,10)]
    public int range;
    public bool canRangeUpgrade;
    [Range(1,10)]
    public int damage;
    public bool canDamageUpgrade;
    [Range(1,10)]
    public int fireRate;
    public bool canFireRateUpgrade;
    [Range(1,10)]
    public int ammo;
    public bool canAmmoUpgrade;

    public string discription;

    //[Header("Dynamic Data")]
    public DynamicWeaponData dynamicData;
}
