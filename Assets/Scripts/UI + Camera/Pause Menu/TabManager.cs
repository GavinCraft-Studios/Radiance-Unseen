using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TabManager : MonoBehaviour
{
    public string selectedTab;
    public Sprite deactivated;
    public Sprite activated;
    public AudioSource sound;

    public Canvas travelCanvas;
    public Canvas gearCanvas;
    public Canvas dataCanvas;
    public Canvas settingCanvas;
    public Canvas menuCanvas;

    public Image travelImg;
    public Image gearImg;
    public Image dataImg;
    public Image settingImg;
    public Image menuImg;

    void Awake()
    {
        selectedTab = "travel";

        travelCanvas.enabled = true;
        gearCanvas.enabled = false;
        dataCanvas.enabled = false;
        settingCanvas.enabled = false;
        menuCanvas.enabled = false;

        travelImg.overrideSprite = activated;
        gearImg.overrideSprite = deactivated;
        dataImg.overrideSprite = deactivated;
        settingImg.overrideSprite = deactivated;
        menuImg.overrideSprite = deactivated;
    }

    public void selectTravel()
    {
        selectedTab = "travel";
        sound.Play();

        travelCanvas.enabled = true;
        gearCanvas.enabled = false;
        dataCanvas.enabled = false;
        settingCanvas.enabled = false;
        menuCanvas.enabled = false;

        travelImg.overrideSprite = activated;
        gearImg.overrideSprite = deactivated;
        dataImg.overrideSprite = deactivated;
        settingImg.overrideSprite = deactivated;
        menuImg.overrideSprite = deactivated;
    }

    public void selectGear()
    {
        selectedTab = "gear";
        sound.Play();

        travelCanvas.enabled = false;
        gearCanvas.enabled = true;
        dataCanvas.enabled = false;
        settingCanvas.enabled = false;
        menuCanvas.enabled = false;

        travelImg.overrideSprite = deactivated;
        gearImg.overrideSprite = activated;
        dataImg.overrideSprite = deactivated;
        settingImg.overrideSprite = deactivated;
        menuImg.overrideSprite = deactivated;
    }

    public void selectData()
    {
        selectedTab = "data";
        sound.Play();

        travelCanvas.enabled = false;
        gearCanvas.enabled = false;
        dataCanvas.enabled = true;
        settingCanvas.enabled = false;
        menuCanvas.enabled = false;

        travelImg.overrideSprite = deactivated;
        gearImg.overrideSprite = deactivated;
        dataImg.overrideSprite = activated;
        settingImg.overrideSprite = deactivated;
        menuImg.overrideSprite = deactivated;
    }

    public void selectSetting()
    {
        selectedTab = "setting";
        sound.Play();

        travelCanvas.enabled = false;
        gearCanvas.enabled = false;
        dataCanvas.enabled = false;
        settingCanvas.enabled = true;
        menuCanvas.enabled = false;

        travelImg.overrideSprite = deactivated;
        gearImg.overrideSprite = deactivated;
        dataImg.overrideSprite = deactivated;
        settingImg.overrideSprite = activated;
        menuImg.overrideSprite = deactivated;
    }

    public void selectMenu()
    {
        selectedTab = "toMenu";
        sound.Play();

        travelCanvas.enabled = false;
        gearCanvas.enabled = false;
        dataCanvas.enabled = false;
        settingCanvas.enabled = false;
        menuCanvas.enabled = true;

        travelImg.overrideSprite = deactivated;
        gearImg.overrideSprite = deactivated;
        dataImg.overrideSprite = deactivated;
        settingImg.overrideSprite = deactivated;
        menuImg.overrideSprite = activated;
    }
}
