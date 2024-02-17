using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeInOut : MonoBehaviour
{
    private bool fadeOut, fadeIn;
    public float fadeSpeed;
    public bool destroyOnFadeOut = false;
    public AudioSource audioOnDestroyOLD;

    public void FadeOutObject()
    {
        fadeOut = true;
    }

    public void FadeInOnbject()
    {
        fadeIn = true;
    }

    void Update()
    {
        if (fadeOut)
        {
            Color objectColor = this.gameObject.GetComponent<SpriteRenderer>().material.color;
            float fadeAmmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;

            if (objectColor.a <= 0)
            {
                fadeOut = false;
                if (destroyOnFadeOut == true)
                {
                    StartCoroutine(destroyThis());
                }
            }
        }

        if (fadeIn)
        {
            Color objectColor = this.gameObject.GetComponent<SpriteRenderer>().material.color;
            float fadeAmmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmmount);
            this.GetComponent<SpriteRenderer>().material.color = objectColor;

            if (objectColor.a >= 1)
            {
                fadeIn = false;
            }
        }
    }

    IEnumerator destroyThis()
    {
        if (audioOnDestroyOLD != null)
        {
            audioOnDestroyOLD.Play();
            yield return new WaitForSeconds(audioOnDestroyOLD.clip.length);
        }
        Destroy(this.gameObject);
    }
}
