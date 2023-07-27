using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Slider HealthSlider;
    public float PlayerHealth;
    public int BaseHealth;

    [SerializeField]
    private Slider EnergySlider;
    public float PlayerEnergy;
    public int BaseEnergy;

    public bool sheildOn;
    public GameObject sheild;
    public WeaponManager weaponManager;
    public float energyUsed;

    void Start()
    {
        PlayerHealth = BaseHealth;
        PlayerEnergy = BaseEnergy;
        HealthSlider.maxValue = BaseHealth;
        HealthSlider.minValue = 0f;
        EnergySlider.maxValue = BaseEnergy;
        EnergySlider.minValue = 0f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet" && !sheildOn == true)
        {
            LaserBullet lsr = other.gameObject.GetComponent<LaserBullet>();
            if (PlayerEnergy >= lsr.damage)
            {
                PlayerEnergy -= lsr.damage;
            }
            else
            {
                PlayerHealth -= lsr.damage;
            }
        }
    }

    void Update()
    {
        HealthSlider.value = PlayerHealth;
        EnergySlider.value = PlayerEnergy;

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
}
