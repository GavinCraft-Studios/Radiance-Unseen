using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_nd_Shooting : MonoBehaviour
{
    public string WeaponType;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 0.5f;
    private float lastShot = 0f;

    public float bulletDamage;
    public float range;

    [SerializeField] private float ammo;
    public float additiveValue;
    [SerializeField] private bool isShooting;

    public float reloadRate = 0.05f;
    private float lastReload = 0f;

    public Sprite Ammo1;
    public Sprite Ammo2;
    public Sprite Ammo3;
    public Sprite Ammo4;
    public Sprite Ammo5;
    public Sprite Ammo6;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Time.time > fireRate + lastShot && ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            LaserBullet bulletScr = bullet.GetComponent<LaserBullet>();
            bulletScr.damage = bulletDamage;
            bulletScr.bulletRange = range;

            ammo = ammo - additiveValue;
            lastShot = Time.time;
            isShooting = true;
        }
        else if (!Input.GetMouseButton(0))
        {
            isShooting = false;
        }
        
        if (ammo < 60 && isShooting == false && Time.time > reloadRate + lastReload)
        {
            ammo += additiveValue;
            lastReload = Time.time;
        }
        else if (ammo > 60 && isShooting == false)
        {
            ammo = 60;
        }

        if (ammo <= 60 && ammo > 50)
        {
            spriteRenderer.sprite = Ammo1;
        }
        else if (ammo <= 50 && ammo > 40)
        {
            spriteRenderer.sprite = Ammo2;
        }
        else if (ammo <= 40 && ammo > 30)
        {
            spriteRenderer.sprite = Ammo3;
        }
        else if (ammo <= 30 && ammo > 20)
        {
            spriteRenderer.sprite = Ammo4;
        }
        else if (ammo <= 20 && ammo > 10)
        {
            spriteRenderer.sprite = Ammo5;
        }
        else if (ammo <= 10 && ammo >= 0)
        {
            spriteRenderer.sprite = Ammo6;
        }
    }
}
