using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class PauseOS_Keybinds : MonoBehaviour
{
    public TMP_Text buttonLabel;
    private KeyCode selectedKeyCode;
    
    public int dataSelector;
    private KeycodeDatabase keycodeDatabase;

    void Start()
    {
        keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();

        selectedKeyCode = keycodeDatabase.GetKeycodeInDatabase(dataSelector);
        buttonLabel.text = selectedKeyCode.ToString();
    }

    public void ChangeKey()
    {
        buttonLabel.text = "-<{ }>-";
        buttonLabel.color = new Color32(64, 138, 255, 255);
    }

    void Update()
    {
        if (buttonLabel.text == "-<{ }>-") {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(keyCode)) {
                    // Update Keycode
                    buttonLabel.text = keyCode.ToString();
                    selectedKeyCode = keyCode;
                    keycodeDatabase.SetKeycodeInDatabase(dataSelector, selectedKeyCode);

                    buttonLabel.color = new Color32(255, 255, 255, 255);
                }
            }
        }
    }
}
