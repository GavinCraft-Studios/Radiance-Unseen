using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeThrow : MonoBehaviour
{
    [Header("Grenade Refs.")]
    public GameObject grenadePrefab;
    public float forceMultiplier;
    public GameObject weaponManagerOb;
    private WeaponManager weaponManager;

    public float throwRate = 2f;
    private float lastThrow = 0f;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    private SpriteRenderer sr;

    void Awake()
    {
        keycodeDatabase = GameObject.Find("Keybinds (TMP)").GetComponent<KeycodeDatabase>();
        keybinds = keycodeDatabase.GetFullDictionary();

        weaponManager = weaponManagerOb.GetComponent<WeaponManager>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        keybinds = keycodeDatabase.GetFullDictionary();

        if (Input.GetKey(keybinds[7]) && Time.time > throwRate + lastThrow && weaponManager.GrenadeCount >= 1)
        {
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
            Rigidbody2D rigidbody2D = grenade.GetComponent<Rigidbody2D>();

            //Mouse Direction
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Vector3)(Input.mousePosition-screenPoint);
            direction.Normalize();
            

            rigidbody2D.AddForce(direction * forceMultiplier, ForceMode2D.Impulse);
            lastThrow = Time.time;
            weaponManager.GrenadeCount--;
        }

        if (weaponManager.GrenadeCount <= 0)
        {
            sr.enabled = false;
        }
    }
}
