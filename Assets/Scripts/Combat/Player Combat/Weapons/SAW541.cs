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
    private int Iterations = 0;
    private int pastIterations;
    private SpriteRenderer spr;

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
        if (Input.GetMouseButton(0) && isLoaded == true)
        {
            StartCoroutine(playCharge());
        }
    }

    IEnumerator playCharge()
    {
        animationState = 1;
        foreach (Sprite frame in charge)
        {
            spr.sprite = frame;
            yield return new WaitForSeconds(chargeInterval);
        }
        lastShot = Time.time;
        isLoaded = false;
        StartCoroutine(playReload());
    }

    IEnumerator playReload()
    {
        animationState = 2;
        for (Iterations = pastIterations; Iterations > reloadTime; Iterations++)
        {
            if (Iterations % 2 == 0)
            {
                spr.sprite = reload[0];
            }
            else
            {
                spr.sprite = reload[1];
            }
            yield return new WaitForSeconds(1f);
        }
        isLoaded = true;
        animationState = 0;
        spr.sprite = defaultImage;
        pastIterations = 0;
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
        }
        pastIterations = Iterations;
    }
}
