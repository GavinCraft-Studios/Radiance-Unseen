using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data.Common;

using UnityEngine.SceneManagement;

public class PauseOS_MenuSelection : MonoBehaviour
{
    [Header("Important Stuff")]
    public List<TMP_Text> options;
    public List<GameObject> gameOptions;

    [Header("Less Important Stuff")]
    public List<string> optionsText;
    public List<bool> hasTrigger;
    public int selected;

    private Color white = Color.white;
    private Color blue = new Color(0.2515723f, 0.5407377f, 1f, 1f);

    public float updateRate = 0.25f;
    private float lastUpdate = 0f;

    private PauseOS_Controller pauseOS;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    void Awake()
    {
        pauseOS = GameObject.Find("Pause OS").GetComponent<PauseOS_Controller>();

        for (int i = 0; i < 7; i++)
        {
            //Debug.Log(i);
            optionsText[i] = options[i].text;
            gameOptions[i].SetActive(false);
        }
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
                AudioManager.instance.PlayOneShot(FMODEvents.instance.osSelect, this.transform.position);
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
                AudioManager.instance.PlayOneShot(FMODEvents.instance.osSelect, this.transform.position);
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

            if (Input.GetKey(keybinds[3]) && Time.realtimeSinceStartup > updateRate + lastUpdate)
            {
                enterOption();
                lastUpdate = Time.realtimeSinceStartup;
            }

            for (int i = 0; i < 7; i++)
            {
                if (selected == i)
                {
                    gameOptions[i].SetActive(true);
                }
                else
                {
                    gameOptions[i].SetActive(false);
                }
            }
        }

        foreach (TMP_Text text in options)
        {
            if(options.IndexOf(text) == selected)
            {
                //text.fontStyle = FontStyles.Underline;
                text.color = blue;
                if (hasTrigger[options.IndexOf(text)])
                {
                    text.text = optionsText[selected] + ">";
                }
            }
            else
            {
                //text.fontStyle = FontStyles.Normal;
                text.color = white;
                text.text = optionsText[options.IndexOf(text)];
            }
        }
    }

    void enterOption()
    {
        switch (selected)
        {
            case 0: 
            pauseOS.resume();
            break;

            case 6:
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Main Menu");
            break;

            default:
            Debug.Log("No Case is Present for " + selected);
            break;
        }
    }
}
