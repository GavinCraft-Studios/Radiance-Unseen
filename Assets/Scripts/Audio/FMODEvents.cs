using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]

    public bool useAmbience;
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference music { get; private set;}
    public List<string> tracks;

    [field: Header("Player SFX")]

    [field: SerializeField] public EventReference footsteps { get; private set; }

    [field: Header("Projectile SFX")]

    [field: SerializeField] public EventReference laserShoot { get; private set; }
    [field: SerializeField] public EventReference laserCollide { get; private set; }
    [field: SerializeField] public EventReference rocketSustain { get; private set; }
    [field: SerializeField] public EventReference rocketExplode { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMODEvents instance in this scene.");
        }
        instance = this;
    }
}