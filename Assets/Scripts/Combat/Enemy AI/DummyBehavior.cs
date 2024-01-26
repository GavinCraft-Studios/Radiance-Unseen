using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehavior : MonoBehaviour
{
    [Header("Time")]
    public float animTime;

    [Header("Animation")]
    public List<Sprite> nonHit;
    public List<Sprite> hit;

    private int animationState;

    private EnemyHealthManager enemyHealthManager;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationState = 0;
        
    }

    void Start()
    {
        StartCoroutine(anim());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        spriteRenderer.sprite = hit[animationState];
    }

    void Update()
    {
        if (enemyHealthManager.enemyHealth != 1)
        {
            // health changed
            spriteRenderer.sprite = hit[animationState];
        }
        enemyHealthManager.enemyHealth = 1;
    }

    IEnumerator anim()
    {
        if(animationState > -1 && animationState < 4) {spriteRenderer.sprite = nonHit[animationState];}
        yield return new WaitForSeconds(animTime / 4);
        animationState += 1;
        if (animationState == 4)
        {
            animationState = 0;
        }
        StartCoroutine(anim());
    }
}
