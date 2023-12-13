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

    // Scope
    public GameObject CameraManageObj;
    private CameraManager cameraManager;

    // Keycodes
    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keycodeDic;

    // Camera Rotation
    public Transform cameraRotation;
    private ArmPivot armPivot;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        cameraManager = CameraManageObj.GetComponent<CameraManager>();
        spr.sprite = defaultImage;
        armPivot = GameObject.Find("Mech Arm Pivot").GetComponent<ArmPivot>();

        keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();
    }

    void Update()
    {
        keycodeDic = keycodeDatabase.GetFullDictionary();

        if (Input.GetMouseButton(0) && canShoot == true)
        {
            canShoot = false;
            StartCoroutine("shoot");
        }

        /*if (armPivot.isFlipped)
        {
            cameraRotation.localRotation = Quaternion.Euler(180, 180, 0);
        }
        else
        {
            cameraRotation.localRotation = Quaternion.Euler(0, 0, 0);
        }*/

        if (cameraManager.initialCameraID != 1 && Input.GetKeyDown(keycodeDic[7]))
        {
            cameraManager.changeCamera(1);
        }
        else if (cameraManager.initialCameraID != 0 && Input.GetKeyUp(keycodeDic[7]))
        {
            cameraManager.changeCamera(0);
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

    void OnDisable()
    {
        canShoot = true;
        spr.sprite = defaultImage;
    }
}
