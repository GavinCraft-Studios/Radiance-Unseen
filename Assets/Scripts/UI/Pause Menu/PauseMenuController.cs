using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public AudioSource alsoAudio;
    public AudioSource Bah;
    public Animator anim;
    private bool Once;
    public CanvasGroup cg;
    public SpriteRenderer sltbg;

    void Awake()
    {
        anim.Play("Opened");
        sltbg.enabled = false;
        cg.alpha = 0f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Time.timeScale == 1f && Once == false)
        {
            Time.timeScale = 0f;
            Once = true;
            alsoAudio.Play();
            anim.Play("Close");
            StartCoroutine("waitForClosed");
        }
        else if (Input.GetKey(KeyCode.Escape) && Time.timeScale == 0f && Once == false)
        {
            Time.timeScale = 1f;
            sltbg.enabled = false;
            cg.alpha = 0f;
            Bah.Play();
            Once = true;
            alsoAudio.Play();
            anim.Play("Open");
            StartCoroutine("waitForOpened");
        }
    }

    IEnumerator waitForClosed()
    {
        yield return new WaitForSecondsRealtime(2f);
        anim.Play("Closed");
        sltbg.enabled = true;
        Once = false;
        cg.alpha = 1f;
        Bah.Play();
    }

    IEnumerator waitForOpened()
    {
        yield return new WaitForSecondsRealtime(2f);
        anim.Play("Opened");
        Once = false;
    }
}
