using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAW541 : MonoBehaviour
{
    public string WeaponType;
    public Transform firePoint;
    public GameObject rocketPrefab;

    private bool isLoaded = true;
    private int animationState;
    private SpriteRenderer spr;
    private bool once;

    private float timestamp;
    private bool takenTime = false;

    public Sprite defaultImage;
    public List<Sprite> charge;
    public float chargeInterval;
    public List<Sprite> reload;
    public int reloadTime;
    private float lastShot;

    void Awake()
    {
        animationState = 1;
        spr = this.gameObject.GetComponent<SpriteRenderer>();
        spr.sprite = defaultImage;
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isLoaded == true && once == false)
        {
            StartCoroutine(playCharge());
        }
    }

    IEnumerator playCharge()
    {
        animationState = 1;
        once = true;
        foreach (Sprite frame in charge)
        {
            spr.sprite = frame;
            yield return new WaitForSeconds(chargeInterval);
        }
        lastShot = Time.time;
        Instantiate(rocketPrefab, transform.position, transform.rotation);
        isLoaded = false;
        once = false;
        StartCoroutine(playReload());
    }

    IEnumerator playReload()
    {
        //Debug.Log("Reload Triggered");
        animationState = 2;

        if (takenTime == false) {timestamp = Time.time; takenTime = true;}
        while (Time.time < timestamp + reloadTime)
        {
            //Debug.Log("In While Loop");
            if (spr.sprite == reload[1] || spr.sprite != reload[0] && spr.sprite != reload[1])
            {
                spr.sprite = reload[0];
            }
            else if (spr.sprite == reload[0])
            {
                spr.sprite = reload[1];
            }
            yield return new WaitForSeconds(1f);
        }

        isLoaded = true;
        animationState = 0;
        spr.sprite = defaultImage;
        takenTime = false;
    }

    void OnEnable()
    {
        if (animationState == 2)
        {
            StartCoroutine(playReload());
        }
    }

    void OnDisable()
    {
        if (animationState == 1)
        {
            spr.sprite = defaultImage;
            animationState = 0;
            once = false;
        }
    }
}
