using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // this is a standardized script for all enemy ai so that things such as grenades can find this standard class
    [Header("Health")]
    public float enemyHealth;

    [Header("Satus")]
    public bool stunned = false;

    [Header("UI")]
    public float alphaValue;
}
