using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data.Common;

public class PauseOS_Options : MonoBehaviour
{
    public List<TMP_Text> options;
    public int selected;

    public float updateRate = 0.25f;
    private float lastUpdate = 0f;

    private PauseOS_Controller pauseOS;
    private SoundManager soundManager;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    void Awake()
    {
        pauseOS = GameObject.Find("Pause OS").GetComponent<PauseOS_Controller>();
        soundManager = GameObject.Find("OS SoundManager").GetComponent<SoundManager>();
    }

    public void ResetSelected() {this.selected = 0;}

    void Update()
    {
        // Instantiate The Database
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();
        }
        this.keybinds = keycodeDatabase.GetFullDictionary();

        if (pauseOS.isFullyOpen)
        {
            if (Input.GetKey(keybinds[0]) && Time.realtimeSinceStartup > updateRate + lastUpdate)
            {
                //Debug.Log("Up");
                soundManager.PlayAudioFromList(0, 0.5f, soundManager.getRandomPitch(2.75f, 3f));
                selected--;

                if (selected < 0)
                {
                    selected = 0;
                }
                else if (selected > options.Count - 1)
                {
                    selected = options.Count - 1;
                }
                lastUpdate = Time.realtimeSinceStartup;
            }
            else if (Input.GetKey(keybinds[1]) && Time.realtimeSinceStartup > updateRate + lastUpdate)
            {
                //Debug.Log("Down");
                soundManager.PlayAudioFromList(0, 0.5f, soundManager.getRandomPitch(2.75f,3f));
                selected++;

                if (selected < 0)
                {
                    selected = options.Count - 1;
                }
                else if (selected > options.Count - 1)
                {
                    selected = 0;
                }
                lastUpdate = Time.realtimeSinceStartup;
            }
        }

        foreach (TMP_Text text in options)
        {
            if(options.IndexOf(text) == selected)
            {
                text.fontStyle = FontStyles.Underline;
            }
            else
            {
                text.fontStyle = FontStyles.Normal;
            }
        }
    }
}
