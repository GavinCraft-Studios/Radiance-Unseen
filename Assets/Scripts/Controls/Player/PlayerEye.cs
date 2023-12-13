using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEye : MonoBehaviour
{
    public GameObject weaponManagerOb;
    private WeaponManager weaponManager;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 0.5f;
    private float lastShot = 0f;

    public float bulletDamage;

    //public Slider energyBar;
    public GameObject player;
    private PlayerController playerController;
    public int energyCost;

    void Awake()
    {
        weaponManager = weaponManagerOb.GetComponent<WeaponManager>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (weaponManager.noWeapons == true || weaponManager.eyeActive == true)
        {
            if (Input.GetMouseButton(0) && Time.time > fireRate + lastShot && playerController.PlayerEnergy >= energyCost)
            {
                playerController.PlayerEnergy -= energyCost;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                LaserBullet bulletScr = bullet.GetComponent<LaserBullet>();
                bulletScr.damage = bulletDamage;
                lastShot = Time.time;
            }
        }
    }
}
