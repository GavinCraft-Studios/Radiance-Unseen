using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject powercellObject;
    public Slider healthBar;
    public Slider energyBar;
    public Image icons;
    public Image MainWeaponUI;
    public Image SubWeaponUI;
    public List<Image> GrenadeWeaponUI;
    public TMP_Text powercellCountText;

    [Header("Sprites")]
    public List<Sprite> iconSprites;

    public void SelectWeapon(int selection)
    {
        icons.sprite = iconSprites[selection];
    }

    public void SetMax(float maxHealth, float maxEnergy)
    {
        healthBar.maxValue = maxHealth;
        energyBar.maxValue = maxEnergy;
    }

    public void SetWeapons(Sprite mainWeapon, Sprite subWeapon, Sprite Grenade, int grenadeCount, int powercellCount)
    {
        MainWeaponUI.sprite = mainWeapon;
        SubWeaponUI.sprite = subWeapon;

        // Set Counts
        SetGrenadeCount(grenadeCount, Grenade);
        SetPowercellCount(powercellCount);
    }

    public void SetGrenadeCount(int grenadeCount, Sprite Grenade)
    {
        if (Grenade != null)
        {
            GrenadeWeaponUI[0].sprite = Grenade;
            GrenadeWeaponUI[1].sprite = Grenade;
            GrenadeWeaponUI[2].sprite = Grenade;
        }

        if (grenadeCount == 0){GrenadeWeaponUI[0].gameObject.GetComponent<Image>().enabled = false; GrenadeWeaponUI[1].gameObject.GetComponent<Image>().enabled = false; GrenadeWeaponUI[2].gameObject.GetComponent<Image>().enabled = false;}
        if (grenadeCount > 0) {GrenadeWeaponUI[0].gameObject.GetComponent<Image>().enabled = true; GrenadeWeaponUI[1].gameObject.GetComponent<Image>().enabled = false; GrenadeWeaponUI[2].gameObject.GetComponent<Image>().enabled = false;}
        if (grenadeCount > 1) {GrenadeWeaponUI[1].gameObject.GetComponent<Image>().enabled = true; GrenadeWeaponUI[1].gameObject.GetComponent<Image>().enabled = true; GrenadeWeaponUI[2].gameObject.GetComponent<Image>().enabled = false;}
        if (grenadeCount > 2) {GrenadeWeaponUI[2].gameObject.GetComponent<Image>().enabled = true; GrenadeWeaponUI[1].gameObject.GetComponent<Image>().enabled = true; GrenadeWeaponUI[2].gameObject.GetComponent<Image>().enabled = true;}
    }

    public void SetPowercellCount(int powercellCount)
    {
        string textCount = powercellCount.ToString();
        powercellCountText.text = textCount;

        if (powercellCount == 0)
        {powercellObject.SetActive(true);}
        else
        {powercellObject.SetActive(false);}
    }

    public void SetEnergy(float energy)
    {
        energyBar.value = energy;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
}
