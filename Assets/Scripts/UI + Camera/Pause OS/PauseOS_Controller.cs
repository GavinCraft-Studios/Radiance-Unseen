using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOS_Controller : MonoBehaviour
{
    public bool isOpen;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    public float updateRate = 0.5f;
    private float lastUpdate = 0f;

    void Update()
    {
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();
        }
        this.keybinds = keycodeDatabase.GetFullDictionary();

        if (isOpen && Input.GetKey(keybinds[9]) && Time.time > updateRate + lastUpdate)
        {
            isOpen = false;
            reloadOS();
            lastUpdate = Time.time;
        }
        else if (Input.GetKey(keybinds[9]) && Time.time > updateRate + lastUpdate)
        {
            isOpen = true;
            reloadOS();
            lastUpdate = Time.time;
        }
    }

    public void reloadOS()
    {
        if (isOpen)
        {

        }
    }
}
