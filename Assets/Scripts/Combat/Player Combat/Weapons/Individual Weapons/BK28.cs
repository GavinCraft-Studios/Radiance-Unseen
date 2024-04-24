using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BK28 : MonoBehaviour
{
    public string WeaponType;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 0.5f;
    private float lastShot = 0f;

    public float bulletDamage;
    public float range;

    public int maxAmmo;
    private int ammo;

    private bool isShooting;
    private bool canShoot;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keycodeDic;

    private SpriteRenderer sr;

    // Sprites
    public Sprite withAmmo;
    public Sprite withoutAmmo;

    public Sprite reload1;
    public Sprite reload2;
    public Sprite reload3;
    public Sprite reload4;
    public Sprite reload5;

    public float reloadTime;

    void Awake()
    {
        keycodeDatabase = GameObject.Find("KeyCode Database").GetComponent<KeycodeDatabase>();
        sr = GetComponent<SpriteRenderer>();

        canShoot = true;
        ammo = maxAmmo;
    }

    void FixedUpdate()
    {
        keycodeDic = keycodeDatabase.GetFullDictionary();

        if (Input.GetMouseButton(0) && Time.time > fireRate + lastShot && ammo > 0 && canShoot == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            LaserBullet bulletScr = bullet.GetComponent<LaserBullet>();
            bulletScr.damage = bulletDamage;
            bulletScr.bulletRange = range;

            ammo -= 1;
            lastShot = Time.time;
            isShooting = true;
        }
        else if (!Input.GetMouseButton(0))
        {
            isShooting = false;
        }

        // if weapon runs out of ammo, change sprite
        if (canShoot == true && ammo != 0)
        {
            sr.sprite = withAmmo;
        }
        else if (canShoot == true && ammo == 0)
        {
            sr.sprite = withoutAmmo;
        }

        // if not shooting check if weapon interact pressed
        if (isShooting == false && Input.GetKey(keycodeDic[7]))
        {
            canShoot = false;
            StartCoroutine("reload");
        }
    }

    IEnumerator reload()
    {
        sr.sprite = reload1;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload2;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload3;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload4;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload5;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload4;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload3;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload2;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        sr.sprite = reload1;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lowBuzz, this.transform.position);
        yield return new WaitForSeconds(reloadTime / 9);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.restore, this.transform.position);
        ammo = maxAmmo;
        canShoot = true;
    }
}
