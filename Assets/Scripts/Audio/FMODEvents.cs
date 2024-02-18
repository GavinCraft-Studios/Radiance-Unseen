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

    // -------------------------------------- { Sound Effects (SFX) } --------------------------------------

    [field: Header("Player SFX")]

    [field: SerializeField] public EventReference footsteps { get; private set; }
    [field: SerializeField] public EventReference shield { get; private set; }

    [field: Header("PauseOS SFX")]

    [field: SerializeField] public EventReference osOpen { get; private set; }
    [field: SerializeField] public EventReference osClose { get; private set; }
    [field: SerializeField] public EventReference osSelect { get; private set; }

    [field: Header("Projectile SFX")]

    [field: SerializeField] public EventReference laserShoot { get; private set; }
    [field: SerializeField] public EventReference laserCollide { get; private set; }
    [field: SerializeField] public EventReference rocketSustain { get; private set; }
    [field: SerializeField] public EventReference rocketExplode { get; private set; }

    [field: Header("Weapon SFX")]

    [field: SerializeField] public EventReference weaponSwitch { get; private set; }
    [field: SerializeField] public EventReference powercell { get; private set; }
    [field: SerializeField] public EventReference lowBuzz { get; private set; }
    [field: SerializeField] public EventReference highBeep { get; private set; }
    [field: SerializeField] public EventReference overheat { get; private set; }
    [field: SerializeField] public EventReference explosion { get; private set; }
    [field: SerializeField] public EventReference restore { get; private set; }
    [field: SerializeField] public EventReference barbIgnite { get; private set; }
    [field: SerializeField] public EventReference barbIdle { get; private set; }
    [field: SerializeField] public EventReference barbSwipe { get; private set; }


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