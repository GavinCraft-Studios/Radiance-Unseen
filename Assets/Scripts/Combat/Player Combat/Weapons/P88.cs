using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P88 : MonoBehaviour
{
    public string WeaponType;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletRange;
    public float bulletDamage;

    public int shotsTillOverheat;
    private int shots;
    public float timeBetweenShots;
    private float lastShot;
    private bool isOverheated;

    public float overheatTime;

    public Sprite default0;
    public Sprite overH1;
    public Sprite overH2;
    public Sprite overH3;
    public Sprite overH4;
    public Sprite overH5;
    private int State;

    private SpriteRenderer sr;

    private Animator anim;

    public AudioSource overheat;
    public AudioSource restore;
    public AudioSource mmm;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if (isOverheated)
        {
            StartCoroutine("Overheat");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isOverheated == false)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            LaserBullet bulletScr = bullet.GetComponent<LaserBullet>();
            bulletScr.damage = bulletDamage;
            bulletScr.bulletRange = bulletRange;

            shots += 1;
            if (Time.time - lastShot >= timeBetweenShots)
            {
                shots = 0;
            }
            else if (Time.time - lastShot < timeBetweenShots && shots >= shotsTillOverheat)
            {
                //Debug.Log("Overheated");
                isOverheated = true;
                State = 0;
                overheat.Play();
                StartCoroutine("Overheat");
            }
            lastShot = Time.time;
        }
    }

    IEnumerator Overheat()
    {
        if (State == 0)
        {
            sr.sprite = overH1;
            State = 1;
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH2;
            State = 2;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH3;
            State = 3;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH4;
            State = 4;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH5;
            State = 5;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = default0;
            isOverheated = false;
            restore.Play();
            State = 0;
        }
        else if (State == 1)
        {
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH2;
            State = 2;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH3;
            State = 3;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH4;
            State = 4;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH5;
            State = 5;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = default0;
            isOverheated = false;
            restore.Play();
            State = 0;
        }
        else if (State == 2)
        {
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH3;
            State = 3;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH4;
            State = 4;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH5;
            State = 5;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = default0;
            isOverheated = false;
            restore.Play();
            State = 0;
        }
        else if (State == 3)
        {
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH4;
            State = 4;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH5;
            State = 5;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = default0;
            isOverheated = false;
            restore.Play();
            State = 0;
        }
        else if (State == 4)
        {
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = overH5;
            State = 5;
            mmm.Play();
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = default0;
            isOverheated = false;
            restore.Play();
            State = 0;
        }
        else if (State == 5)
        {
            yield return new WaitForSeconds(overheatTime / 5);
            sr.sprite = default0;
            isOverheated = false;
            restore.Play();
            State = 0;
        }
    }
}