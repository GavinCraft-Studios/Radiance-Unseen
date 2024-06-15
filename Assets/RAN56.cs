using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAN56 : DefaultWeapon
{
    [Header("RAN-56")]
    public string WeaponType;
    public Transform firePoint;
    public GameObject arrowPrefab;

    [Header("Visuals")]
    private SpriteRenderer spriteRenderer;
    public ParticleSystem chargeEffect;

    // Animations
    public Sprite defaultSprite;
    public int animState;
    public float idleWait;

    public List<Sprite> initAnim;
    public float initAnimTime;
    public List<Sprite> idleAnim;
    public float idleAnimTime;
    public List<Sprite> chrgAnim;
    public List<Sprite> chdlAnim;

    protected override void Awake()
    {
        // Call Default Awake
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        StartCoroutine(InitializeAnim());
    }

    IEnumerator InitializeAnim()
    {
        animState = 1;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.barbIgnite, transform.position);
        foreach (Sprite sprite in initAnim)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(initAnimTime/initAnim.Count);
        }
        spriteRenderer.sprite = defaultSprite;
        animState = 0;
        StartCoroutine(IdleAnim());
    }

    IEnumerator IdleWait()
    {
        if (animState == 0)
        {
            yield return new WaitForSeconds(idleWait);
            StartCoroutine(IdleAnim());
        }
    }

    IEnumerator IdleAnim()
    {
        animState = 2;
        foreach (Sprite sprite in idleAnim)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(idleAnimTime/idleAnim.Count);
        }
        spriteRenderer.sprite = defaultSprite;
        animState = 0;
        StartCoroutine(IdleWait());
    }
}
