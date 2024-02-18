using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Selected Weapons by ID")]
    public int MainWeaponID;
    public int SubWeaponID;
    public int GrenadeID;
    public int GrenadeCount;
    public int PowerCellCount;

    [Header("Weapons by Type")]
    public List<GameObject> MainWeapons;
    public List<GameObject> SubWeapons;
    public List<GameObject> Grenades;
    public GameObject PowerCell;

    [Header("Refrences")]
    public GameObject playerHUD;
    private PlayerHUDController playerHUDController;

    [Header("Config")]
    public float switchRate;
    private float lastSwitch;
    public bool noWeapons;
    public bool eyeActive;

    private string SelectedWeapon;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keycodeDic;

    void Awake()
    {
        playerHUDController = playerHUD.GetComponent<PlayerHUDController>();

        //Select Weapon
        SelectedWeapon = "Main";
        playerHUDController.SelectWeapon(0);

        //Setup HUD;
        playerHUDController.SetWeapons(
        MainWeapons[MainWeaponID].GetComponent<WeaponSwitchSprites>().UI,
        SubWeapons[SubWeaponID].GetComponent<WeaponSwitchSprites>().UI,
        Grenades[GrenadeID].GetComponent<WeaponSwitchSprites>().UI,
        GrenadeCount,
        PowerCellCount
        );
    }

    void OnDrawGizmos()
    {
        playerHUDController = playerHUD.GetComponent<PlayerHUDController>();
        playerHUDController.SelectWeapon(0);
        
        playerHUDController.SetWeapons(
        MainWeapons[MainWeaponID].GetComponent<WeaponSwitchSprites>().UI,
        SubWeapons[SubWeaponID].GetComponent<WeaponSwitchSprites>().UI,
        Grenades[GrenadeID].GetComponent<WeaponSwitchSprites>().UI,
        GrenadeCount,
        PowerCellCount
        );
    }

    void Update()
    {
        // Setup
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();
        }
        keycodeDic = keycodeDatabase.GetFullDictionary();
        playerHUDController.SetPowercellCount(PowerCellCount);
        playerHUDController.SetGrenadeCount(GrenadeCount, Grenades[GrenadeID].GetComponent<WeaponSwitchSprites>().UI);


        if (noWeapons == false)
        {
            if (Input.GetKey(keycodeDic[4]) && Time.time > switchRate + lastSwitch)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.weaponSwitch, this.transform.position);
                SelectedWeapon = "Main";
                playerHUDController.SelectWeapon(0);
                eyeActive = false;
                lastSwitch = Time.time;
            }
            else if (Input.GetKey(keycodeDic[5]) && Time.time > switchRate + lastSwitch)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.weaponSwitch, this.transform.position);
                SelectedWeapon = "Sub";
                playerHUDController.SelectWeapon(1);
                eyeActive = false;
                lastSwitch = Time.time;
            }
            else if (Input.GetKey(keycodeDic[6]) && Time.time > switchRate + lastSwitch)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.weaponSwitch, this.transform.position);
                SelectedWeapon = "Grenade";
                playerHUDController.SelectWeapon(2);
                eyeActive = false;
                lastSwitch = Time.time;
            }
            else if (Input.GetKey(keycodeDic[8]) && Time.time > switchRate + lastSwitch)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.weaponSwitch, this.transform.position);
                SelectedWeapon = "PowerCell";
                playerHUDController.SelectWeapon(3);
                eyeActive = false;
                lastSwitch = Time.time;
            }

            if (SelectedWeapon == "Main")
            {
                MainWeapons[MainWeaponID].SetActive(true);
                SubWeapons[SubWeaponID].SetActive(false);
                Grenades[GrenadeID].SetActive(false);
                PowerCell.SetActive(false);
            }
            else if (SelectedWeapon == "Sub")
            {
                MainWeapons[MainWeaponID].SetActive(false);
                SubWeapons[SubWeaponID].SetActive(true);
                Grenades[GrenadeID].SetActive(false);
                PowerCell.SetActive(false);
            }
            else if (SelectedWeapon == "Grenade")
            {
                MainWeapons[MainWeaponID].SetActive(false);
                SubWeapons[SubWeaponID].SetActive(false);
                Grenades[GrenadeID].SetActive(true);
                PowerCell.SetActive(false);
            }
            else if (SelectedWeapon == "PowerCell")
            {
                MainWeapons[MainWeaponID].SetActive(false);
                SubWeapons[SubWeaponID].SetActive(false);
                Grenades[GrenadeID].SetActive(false);
                PowerCell.SetActive(true);
            }
        }
        else if (noWeapons == true)
        {
            foreach (GameObject weapon in MainWeapons)
            {
                if (weapon == null)
                {
                    continue;
                }
                weapon.SetActive(false);
            }

            foreach (GameObject weapon in SubWeapons)
            {
                if (weapon == null)
                {
                    continue;
                }
                weapon.SetActive(false);
            }

            foreach (GameObject weapon in Grenades)
            {
                if (weapon == null)
                {
                    continue;
                }
                weapon.SetActive(false);
            }
        }
    }
}
