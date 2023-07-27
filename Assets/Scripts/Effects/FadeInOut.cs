using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    private bool fadeOut, fadeIn;
    public float fadeSpeed;
    public bool destroyOnFadeOut = false;

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
                    Destroy(this.gameObject);
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

    public void FadeOutObject()
    {
        fadeOut = true;
    }

    public void FadeInOnbject()
    {
        fadeIn = true;
    }
}
