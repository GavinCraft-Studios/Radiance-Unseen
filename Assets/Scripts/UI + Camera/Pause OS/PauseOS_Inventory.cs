using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseOS_Inventory : MonoBehaviour
{
    [Header("Tab Management")]
    private PauseOS_MenuSelection menuSelection;
    // selector is 1
    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    public List<GameObject> tabs;
    public List<Image> underlines;
    private int selected = 0;

    [Header("Display Refs.")]
    public GameObject weaponDisplay;
    public GameObject grenadeDisplay;
    public GameObject gadgetDisplay;

    [Header("Weapon Display Refs.")]   
    public TMP_Text weaponLabel;
    public Image weaponImage;

    public GameObject prevButton;
    public GameObject nextButton;

    public IndvWeaponDisplay range;
    public IndvWeaponDisplay damage;
    public IndvWeaponDisplay fireRate;
    public IndvWeaponDisplay ammo;
    public TMP_Text weaponDiscription;

    [Range(0,5)]
    private int mainSelected = 0;
    [Range(6,11)]
    private int subSelected = 6;

    private WeaponDatabase weaponDatabase;

    private void Awake()
    {
        menuSelection = GameObject.Find("Menu Selection").GetComponent<PauseOS_MenuSelection>();

        // TODO - construct weapon database -------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }

    private void Update()
    {
        // Instantiate The Database
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("KeyCode Database").GetComponent<KeycodeDatabase>();
        }
        this.keybinds = keycodeDatabase.GetFullDictionary();

        if (menuSelection.selected == 1)
        {
            if (Input.GetKeyDown(keybinds[2])) // left
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.osSelect, this.transform.position);
                selected -= 1;
                if (selected < 0)
                {
                    selected = 0;
                }
            }
            else if (Input.GetKeyDown(keybinds[3])) // right
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.osSelect, this.transform.position);
                selected += 1;
                if (selected > 3)
                {
                    selected = 3;
                }
            }

            UpdateMenu();
            // TODO - Update Weapon Manager If there is a change -------------------------------------------------------------------------------------------------------------------------------------------------
        }
    }

    public void UpdateMenu()
    {
        // Tab Selection
        Color color = new Color(0.2515723f, 0.5407377f, 1f, 1f);
        foreach (GameObject tab in tabs)
        {
            int index = tabs.IndexOf(tab);

            TMP_Text text = tabs[index].GetComponentInChildren<TMP_Text>();
            text.color = Color.white;
            underlines[index].color = Color.white;
        }

        TMP_Text Selectedtext = tabs[selected].GetComponentInChildren<TMP_Text>();
        Selectedtext.color = color;
        underlines[selected].color = color;

        // Weapon Statistics Display
        switch (selected)
        {
            case 0:
                weaponDisplay.SetActive(true);
                //grenadeDisplay.SetActive(false);
                //gadgetDisplay.SetActive(false);

                UpdateWeaponMenu(weaponDatabase.getWeaponList()[mainSelected]);
                break;
            case 1:
                weaponDisplay.SetActive(true);
                //grenadeDisplay.SetActive(false);
                //gadgetDisplay.SetActive(false);

                UpdateWeaponMenu(weaponDatabase.getWeaponList()[subSelected]);
                break;
            case 2:
                weaponDisplay.SetActive(false);
                grenadeDisplay.SetActive(true);
                gadgetDisplay.SetActive(false);
                break;
            case 3:
                weaponDisplay.SetActive(false);
                grenadeDisplay.SetActive(false);
                gadgetDisplay.SetActive(true);
                break;
        }
    }

    public void UpdateWeaponMenu(WeaponData selectedWeapon)
    {
        weaponLabel.text = selectedWeapon.displayName;
        weaponImage.sprite = selectedWeapon.sprite;

        // Range
        for (int i = 0; i <= 10; i++) {
            if (i <= selectedWeapon.range) {
                range.stages[i].color = Color.white;
            } else {
                range.stages[i].color = new Color(225,225,225,35);
            }
        }

        // Damage
        for (int i = 0; i <= 10; i++) {
            if (i <= selectedWeapon.damage) {
                damage.stages[i].color = Color.white;
            } else {
                damage.stages[i].color = new Color(225,225,225,35);
            }
        }

        // Fire Rate
        for (int i = 0; i <= 10; i++) {
            if (i <= selectedWeapon.fireRate) {
                fireRate.stages[i].color = Color.white;
            } else {
                fireRate.stages[i].color = new Color(225,225,225,35);
            }
        }

        // Ammo
        for (int i = 0; i <= 10; i++) {
            if (i <= selectedWeapon.ammo) {
                ammo.stages[i].color = Color.white;
            } else {
                ammo.stages[i].color = new Color(225,225,225,35);
            }
        }

        weaponDiscription.text = selectedWeapon.discription;
        if (selected == 0 && mainSelected == 0) {
            prevButton.SetActive(false); nextButton.SetActive(true);
        }
        else if (selected == 0 && mainSelected == 5) {
            prevButton.SetActive(true); nextButton.SetActive(false);
        }
        else if (selected == 1 && subSelected == 6) {
            prevButton.SetActive(false); nextButton.SetActive(true);
        }
        else if (selected == 1 && subSelected == 11) {
            prevButton.SetActive(true); nextButton.SetActive(false);
        }
        else {
            prevButton.SetActive(true); nextButton.SetActive(true);
        }
    }

    // TODO - Make both indexes 1 to 5 ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SelectPrevWeapon() {if(selected == 0) {mainSelected -= 1;} else {subSelected -= 1;}}
    public void SelectNextWeapon() {if(selected == 0) {mainSelected += 1;} else {subSelected += 1;}}
}
