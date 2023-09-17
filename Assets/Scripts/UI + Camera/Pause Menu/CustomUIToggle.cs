using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomUIToggle : MonoBehaviour, IDataPersistance
{
    public Image ButtonImage;
    public Sprite ButtonOff;
    public Sprite ButtonOn;

    public bool defaultState;
    private bool isTriggered;

    public void ButtonTrigger()
    {
        if (isTriggered == false)
        {
            isTriggered = true;
            ButtonImage.overrideSprite = ButtonOn;
        }
        else if (isTriggered == true)
        {
            isTriggered = false;
            ButtonImage.overrideSprite = ButtonOff;
        }

        Screen.fullScreen = isTriggered;
    }

    public void LoadGame(GameData data)
    {
        this.isTriggered = data.fullscreen;
        if (isTriggered == false)
        {
            ButtonImage.overrideSprite = ButtonOff;
        }
        else if (isTriggered == true)
        {
            ButtonImage.overrideSprite = ButtonOn;
        }
    }

    public void SaveGame(GameData data)
    {
        data.fullscreen  = this.isTriggered;
    }
}
