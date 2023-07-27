using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAN38 : MonoBehaviour
{
    public string WeaponType;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool canShoot = true;

    public float bulletDamage;
    public float range;

    private SpriteRenderer spr;

    public Sprite defaultImage;
    public float shootingTime;
    public List<Sprite> shooting;
    public float reloadTime;
    public List<Sprite> reload;

    // Sounds
    public AudioSource explosion;
    public AudioSource decay;
    public AudioSource powerup;
    public AudioSource restore;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = defaultImage;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            canShoot = false;
            StartCoroutine("shoot");
        }
    }

    IEnumerator shoot()
    {
        foreach (Sprite shootin in shooting)
        {
            yield return new WaitForSeconds(0.1f);
            spr.sprite = shootin;
            powerup.Play();
        }

        explosion.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        LaserBullet bulletScr = bullet.GetComponent<LaserBullet>();
        bulletScr.damage = bulletDamage;
        bulletScr.bulletRange = range;
        StartCoroutine("reloading");
    }

    IEnumerator reloading()
    {
        foreach (Sprite relod in reload)
        {
            spr.sprite = relod;
            yield return new WaitForSeconds(reloadTime / reload.Count);
        }
        spr.sprite = defaultImage;
        canShoot = true;
        restore.Play();
    }
}
