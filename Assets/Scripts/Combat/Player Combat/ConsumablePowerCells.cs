using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePowerCells : MonoBehaviour
{
    [Header("Power Cell Refs.")]
    [SerializeField] private GameObject weaponsOb;
    private WeaponManager weaponManager;
    [SerializeField] private GameObject playerOb;
    private PlayerController playerController;
    private AudioSource sfx;
    [SerializeField] private ParticleSystem fx;
    private SpriteRenderer sr;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keycodeDic;

    [Header("Properties")]
    [SerializeField] private float fireRate = 2f;
    private float lastShot = 0f;
    [SerializeField] private float energyHealed;


    void Awake()
    {
        weaponManager = weaponsOb.GetComponent<WeaponManager>();
        playerController = playerOb.GetComponent<PlayerController>();
        sfx = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

        keycodeDatabase = GameObject.Find("Keybinds (TMP)").GetComponent<KeycodeDatabase>();
    }

    void Update()
    {
        keycodeDic = keycodeDatabase.GetFullDictionary();
        if (Input.GetKey(keycodeDic[7]) && Time.time > fireRate + lastShot && weaponManager.PowerCellCount > 0)
        {
            playerController.PlayerEnergy += energyHealed;
            weaponManager.PowerCellCount--;
            if (weaponManager.PowerCellCount == 0)
            {
                sr.enabled = false;
            }
            sfx.Play();
            fx.Play();
            lastShot = Time.time;
        }
    }
}
