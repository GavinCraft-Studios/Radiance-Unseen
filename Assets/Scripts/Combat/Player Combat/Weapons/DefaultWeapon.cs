using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : MonoBehaviour
{
    [Header("Default Weapon")]
    public WeaponData weaponData;

    public float rangeMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float fireRateMultiplier = 1f;
    public float ammoMultiplier = 1f;

    protected virtual void Awake()
    {
        weaponData = this.GetComponent<WeaponData>();
    }

    protected virtual void Update()
    {
        DynamicWeaponData data = weaponData.dynamicData;

        if (data != null) {return;}

        switch(data.rangeUpgrade) {
            case 0: rangeMultiplier = 1; break;
            case 1: rangeMultiplier = 1.15f; break;
            case 2: rangeMultiplier = 1.3f; break;
            case 3: rangeMultiplier = 1.5f; break;
        }

        switch(data.damageUpgrade) {
            case 0: damageMultiplier = 1; break;
            case 1: damageMultiplier = 1.15f; break;
            case 2: damageMultiplier = 1.3f; break;
            case 3: damageMultiplier = 1.5f; break;
        }

        switch (data.fireRateUpgrade) {
            case 0: fireRateMultiplier = 1; break;
            case 1: fireRateMultiplier = 0.9f; break;
            case 2: fireRateMultiplier = 0.8f; break;
            case 3: fireRateMultiplier = 0.7f; break;
        }

        switch (data.ammoUpgrade) {
            case 0: ammoMultiplier = 1; break;
            case 1: ammoMultiplier = 1.1f; break;
            case 2: ammoMultiplier = 1.2f; break;
            case 3: ammoMultiplier = 1.3f; break;
        }
    }
}
