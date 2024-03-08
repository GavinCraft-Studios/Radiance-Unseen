using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOS_Settings : MonoBehaviour//, IDataPersistance
{
    private GameObject verticleGroupObject;

    private Slider[] volumeSliders;
    private PauseOS_Keybinds[] pauseOS_Keybinds;

    private Toggle fullscreenToggle;

    void Start()
    {
        verticleGroupObject = GameObject.Find("Verticle Group (Pause OS)");

        // Settings Components
        pauseOS_Keybinds = GetComponentsInChildren<PauseOS_Keybinds>();
        volumeSliders = verticleGroupObject.GetComponentsInChildren<Slider>();
    }

    public void ResetToDefaults()
    {
        GameData templateData = new GameData();

        foreach (PauseOS_Keybinds keybind in pauseOS_Keybinds)
        {
            keybind.ChangeKey(templateData.keycodeDatabase[keybind.dataSelector]);
        }

        foreach (Slider slider in volumeSliders)
        {
            slider.value = templateData.masterVolume;
        }

        //fullscreenToggle. 
    }
}
