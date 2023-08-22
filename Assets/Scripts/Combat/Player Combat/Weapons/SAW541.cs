using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAW541 : MonoBehaviour
{
    public string WeaponType;
    public Transform firePoint;
    public GameObject rocketPrefab;

    private bool isLoaded;
    private int animationState;
    private SpriteRenderer spr;

    public Sprite defaultImage;
    public List<Sprite> charge;
    public float chargeInterval;
    public List<Sprite> reload;
    public float reloadTime;
    private float lastShot;

    void Awake()
    {
        animationState = 1;
        spr = this.gameObject.GetComponent<SpriteRenderer>();
        spr.sprite = defaultImage;
        
    }

    void Update()
    {

    }

    IEnumerator playCharge()
    {
        animationState = 1;
        foreach (Sprite frame in charge)
        {
            spr.sprite = frame;
            yield return new WaitForSeconds(chargeInterval);
        }
        playReload();
    }

    void playReload()
    {
        animationState = 2;
        lastShot = Time.time;
        while (lastShot + reloadTime >= Time.time) {return;}
    }

    void OnDisable()
    {
        if (animationState == 1)
        {
            spr.sprite = defaultImage;
            animationState = 0;
        }
    }
}
