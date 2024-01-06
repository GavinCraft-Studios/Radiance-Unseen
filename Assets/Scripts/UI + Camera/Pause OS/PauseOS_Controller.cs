using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOS_Controller : MonoBehaviour
{
    [Header("State")]
    public bool isOpen;
    public bool isOpenUpdated;

    [Header("References")]
    public CanvasGroup cg;
    public SpriteRenderer ocRend;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    [Header("Config")]
    public float updateRate = 1.1f;
    private float lastUpdate = 0f;

    [Header("Animation")]
    public List<Sprite> openclose;
    public List<Sprite> closeopen;
    public float ocAnimTime;

    void Update()
    {
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();
        }
        this.keybinds = keycodeDatabase.GetFullDictionary();

        if (isOpen && Input.GetKey(keybinds[9]) && Time.realtimeSinceStartup > updateRate + lastUpdate)
        {
            isOpen = false;
            isOpenUpdated = true;
            StartCoroutine(reloadOS());
            lastUpdate = Time.realtimeSinceStartup;
        }
        else if (Input.GetKey(keybinds[9]) && Time.realtimeSinceStartup > updateRate + lastUpdate)
        {
            isOpen = true;
            isOpenUpdated = true;
            StartCoroutine(reloadOS());
            lastUpdate = Time.realtimeSinceStartup;
        }

        Debug.Log(Time.timeScale.ToString());
    }

    IEnumerator reloadOS()
    {
        if (isOpen)
        {
            if (isOpenUpdated)
            {
                StartCoroutine(ocAnim(false));
                for (int i = 1; i <= 100; i++)
                {
                    Time.timeScale = i / 100;
                    yield return new WaitForSecondsRealtime(ocAnimTime / 100);
                }
            }
        }
        else
        {
            if (isOpenUpdated)
            {
                StartCoroutine(ocAnim(true));
                for (int i = 100; i > 1; i--)
                {
                    Time.timeScale = i / 100;
                    yield return new WaitForSecondsRealtime(ocAnimTime / 100);
                }
            }
        }

        isOpenUpdated = false;
        yield return new WaitForSecondsRealtime(0.01f);
    }

    IEnumerator ocAnim(bool isOpening)
    {
        if (isOpening)
        {
            foreach (Sprite frame in openclose)
            {
                yield return new WaitForSecondsRealtime(ocAnimTime / openclose.Count);
                ocRend.sprite = frame;
            }
        }
        else
        {
            foreach (Sprite frame in closeopen)
            {
                yield return new WaitForSecondsRealtime(ocAnimTime / closeopen.Count);
                ocRend.sprite = frame;
            }
        }
    }
}
