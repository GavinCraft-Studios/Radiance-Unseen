using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeybindButton : MonoBehaviour
{
    public TMP_Text buttonLabel;
    public Image buttonImage;
    public Sprite deactivatedSprite;
    public Sprite activatedSprite;
    public KeyCode selectedKey;

    [Header("KeyCode Data Selector")]
    public int dataSelector = 0;
    private KeycodeDatabase keycodeDatabase;

    void Awake()
    {
        keycodeDatabase = GameObject.Find("Keybinds (TMP)").GetComponent<KeycodeDatabase>();
    }

    void Start()
    {
        selectedKey = keycodeDatabase.GetKeycodeInDatabase(dataSelector);
        buttonLabel.text = selectedKey.ToString();
    }
    
    public void ChangeKey()
    {
        buttonLabel.text = "-< >-";
        buttonLabel.color = new Color32(242, 199, 56, 255);
        buttonImage.overrideSprite = activatedSprite;
    }

    void Update()
    {
        if (buttonLabel.text == "-< >-")
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    buttonLabel.text = keyCode.ToString();
                    buttonLabel.color = new Color32(18, 181, 199, 255);
                    buttonImage.overrideSprite = deactivatedSprite;
                    selectedKey = keyCode;
                    keycodeDatabase.SetKeycodeInDatabase(dataSelector, selectedKey);
                }
            }
        }
    }
}
