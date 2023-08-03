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
    public GameObject MainDisplay;
    public GameObject MainWeaponDisplay;
    public GameObject SubDisplay;
    public GameObject SubWeaponDisplay;
    public GameObject GrenadeDisplay;
    public GameObject GrenadeWeaponDisplay;
    public Sprite ActiveUI;
    public Sprite DeactiveUI;
    public TMP_Text GrenadeCountTMP;
    public TMP_Text PowercellCountTMP;

    [Header("Config")]
    public float switchRate;
    private float lastSwitch;
    public bool noWeapons;
    public bool eyeActive;

    private string SelectedWeapon;
    private Image MainBG;
    private Image MainDisp;
    private Image SubBG;
    private Image SubDisp;
    private Image GrenadeBG;
    private Image GrenadeDisp;
    private AudioSource SwitchSound;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keycodeDic;



    void Awake()
    {
        MainBG = MainDisplay.GetComponent<Image>();
        MainDisp = MainWeaponDisplay.GetComponent<Image>();
        SubBG = SubDisplay.GetComponent<Image>();
        SubDisp = SubWeaponDisplay.GetComponent<Image>();
        GrenadeBG = GrenadeDisplay.GetComponent<Image>();
        GrenadeDisp = GrenadeWeaponDisplay.GetComponent<Image>();

        SwitchSound = GetComponent<AudioSource>();

        keycodeDatabase = GameObject.Find("Keybinds (TMP)").GetComponent<KeycodeDatabase>();
        UpdateUI();
    }

    void Start()
    {
        SelectedWeapon = "Main";
    }

    public void UpdateUI()
    {
        MainDisp.overrideSprite = MainWeapons[MainWeaponID].GetComponent<WeaponSwitchSprites>().UI;
        SubDisp.overrideSprite = SubWeapons[SubWeaponID].GetComponent<WeaponSwitchSprites>().UI;
        GrenadeDisp.overrideSprite = Grenades[GrenadeID].GetComponent<WeaponSwitchSprites>().UI;
    }

    void Update()
    {
        keycodeDic = keycodeDatabase.GetFullDictionary();
        GrenadeCountTMP.text = GrenadeCount.ToString();
        PowercellCountTMP.text = PowerCellCount.ToString();

        if (noWeapons == false)
        {
            if (Input.GetKey(keycodeDic[4]) && Time.time > switchRate + lastSwitch)
            {
                SwitchSound.Play();
                SelectedWeapon = "Main";
                MainBG.sprite = ActiveUI;
                SubBG.sprite = DeactiveUI;
                GrenadeBG.sprite = DeactiveUI;
                eyeActive = false;
                lastSwitch = Time.time;
            }
            else if (Input.GetKey(keycodeDic[5]) && Time.time > switchRate + lastSwitch)
            {
                SwitchSound.Play();
                SelectedWeapon = "Sub";
                MainBG.sprite = DeactiveUI;
                SubBG.sprite = ActiveUI;
                GrenadeBG.sprite = DeactiveUI;
                eyeActive = false;
                lastSwitch = Time.time;
            }
            else if (Input.GetKey(keycodeDic[6]) && Time.time > switchRate + lastSwitch)
            {
                SwitchSound.Play();
                SelectedWeapon = "Grenade";
                MainBG.sprite = DeactiveUI;
                SubBG.sprite = DeactiveUI;
                GrenadeBG.sprite = ActiveUI;
                eyeActive = false;
                lastSwitch = Time.time;
            }
            else if (Input.GetKey(keycodeDic[8]) && Time.time > switchRate + lastSwitch)
            {
                SwitchSound.Play();
                SelectedWeapon = "PowerCell";
                MainBG.sprite = DeactiveUI;
                SubBG.sprite = DeactiveUI;
                GrenadeBG.sprite = DeactiveUI;
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
                weapon.SetActive(false);
            }

            foreach (GameObject weapon in SubWeapons)
            {
                weapon.SetActive(false);
            }

            foreach (GameObject weapon in Grenades)
            {
                weapon.SetActive(false);
            }
        }
    }
}
