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

    private PauseOS_MenuSelection menuSelection;
    private PauseOS_Controller controller;

    void Start()
    {
        menuSelection = GameObject.Find("Menu Selection").GetComponent<PauseOS_MenuSelection>();
        controller = GameObject.Find("Pause OS").GetComponent<PauseOS_Controller>();

        keycodeDatabase = GameObject.Find("KeyCode Database").GetComponent<KeycodeDatabase>();
        selectedKeyCode = keycodeDatabase.GetKeycodeInDatabase(dataSelector);
        buttonLabel.text = selectedKeyCode.ToString();
    }

    public void ChangeKey()
    {
        buttonLabel.text = "-<{ }>-";
        buttonLabel.color = new Color32(64, 138, 255, 255);

        menuSelection.canChangeSelection = false;
        controller.canChangeState = false;
    }

    public void ChangeKey(KeyCode keyCode)
    {
        buttonLabel.text = keyCode.ToString();
        selectedKeyCode = keyCode;
        keycodeDatabase.SetKeycodeInDatabase(dataSelector, selectedKeyCode);
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
                    StartCoroutine(WaitForDebug());
                }
            }
        }
    }

    IEnumerator WaitForDebug()
    {
        yield return new WaitForSeconds(0.1f);

        menuSelection.canChangeSelection = true;
        controller.canChangeState = true;
    }
}
