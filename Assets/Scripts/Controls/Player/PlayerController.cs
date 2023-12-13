using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Health & Energy")]
    public float PlayerHealth;
    public int BaseHealth;
    public float PlayerEnergy;
    public int BaseEnergy;

    [Header("Sheild")]
    public bool sheildOn;
    public GameObject sheild;
    public WeaponManager weaponManager;
    public float energyUsed;

    [Header("UI")]
    public GameObject UIManager;
    private PlayerHUDController playerHUDController;

    //[Header("Music Management")]
    //public List<AudioClip> music;

    void Start()
    {
        playerHUDController = UIManager.GetComponent<PlayerHUDController>();
        PlayerHealth = BaseHealth;
        PlayerEnergy = BaseEnergy;
        playerHUDController.SetMax(BaseHealth, BaseEnergy);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet" && !sheildOn == true)
        {
            LaserBullet lsr = other.gameObject.GetComponent<LaserBullet>();
            deductor(lsr.damage);
        }
    }

    void Update()
    {
        playerHUDController.SetEnergy(PlayerEnergy);
        playerHUDController.SetHealth(PlayerHealth);
        if (PlayerEnergy > BaseEnergy)
        {
            PlayerEnergy = BaseEnergy;
        }

        if (sheildOn == true)
        {
            sheild.SetActive(true);
            weaponManager.noWeapons = true;
            PlayerEnergy -= energyUsed;
        }
        else
        {
            sheild.SetActive(false);
            weaponManager.noWeapons = false;
        }
    }

    public void deductor(float ammountToRemove)
    {
        float remainder = 0f;
        if (PlayerEnergy > 0)
        {
            if (PlayerEnergy >= ammountToRemove)
            {PlayerEnergy -= ammountToRemove; return;}

            remainder = ammountToRemove - PlayerEnergy;
            PlayerEnergy -= ammountToRemove - remainder;
        }

        if (PlayerHealth > 0)
        {
            if (remainder != 0f)
            {PlayerHealth -= remainder; return;}

            PlayerHealth -= ammountToRemove;
        }
    }

    public void changeMusic(AudioClip clip)
    {

    }
}
