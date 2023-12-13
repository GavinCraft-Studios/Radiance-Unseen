using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BARB : MonoBehaviour
{
    [Header("Attacking")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackDamage;

    [Header("SoundFX")]
    public AudioSource igniteAudio;
    public AudioSource idleAudio;
    public AudioSource attackAudio;

    [Header("Animations")]
    public float IgniteFrameTime;
    public List<Sprite> IgniteFrames;
    public float IdleFrameTime;
    public float IdleTweenTime;
    public List<Sprite> IdleFrames;
    public Sprite defaultImage;
    public float attackTime;
    public Sprite attackFrame;

    public Transform debugArm;

    private int currentAnimation;
    private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("animIgnite");
    }

    void OnDisable()
    {
        spriteRenderer.sprite = IgniteFrames[0];
        if (currentAnimation == 3)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Update()
    {
        if (currentAnimation == 2 && Input.GetMouseButtonDown(0))
        {
            currentAnimation = 3;
            StopAllCoroutines();
            StartCoroutine("animAttack");
            Attack();
        }
    }

    void OnDrawGizmos()
    {
        // Set the color for the circle (you can choose any color you prefer)
        Gizmos.color = Color.red;

        // Draw the circle using the transform position of the GameObject
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(new Vector2(attackPoint.position.x, attackPoint.position.y), attackRange);
        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.gameObject.tag == "Enemy")
            {
                EnemyHealthManager enemyHealthManager = collider2D.gameObject.GetComponent<EnemyHealthManager>();
                enemyHealthManager.enemyHealth -= attackDamage;
                enemyHealthManager.alphaValue = 1;
            }
        }
    }

    IEnumerator animIgnite()
    {
        igniteAudio.Play();
        currentAnimation = 1;
        foreach (Sprite frame in IgniteFrames)
        {
            yield return new WaitForSeconds(IgniteFrameTime);
            spriteRenderer.sprite = frame;
        }
        StartCoroutine("animIdle");
    }

    IEnumerator animIdle()
    {
        currentAnimation = 2;
        spriteRenderer.sprite = defaultImage;
        foreach (Sprite frame in IdleFrames)
        {
            yield return new WaitForSeconds(IdleFrameTime);
            spriteRenderer.sprite = frame;
        }
        yield return new WaitForSeconds(IdleFrameTime);
        spriteRenderer.sprite = defaultImage;
        yield return new WaitForSeconds(IdleTweenTime);
        StartCoroutine("animIdle");
    }

    IEnumerator animAttack()
    {
        //Debug.Log("Attack Corutine Trggered");
        attackAudio.Play();
        currentAnimation = 3;
        spriteRenderer.sprite = attackFrame;
        for (int i = 0; i < 85; i++)
        {
            //Debug.Log("For Loop Triggered");
            yield return new WaitForSeconds(attackTime);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z += - 1f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }

        spriteRenderer.sprite = defaultImage;
        for (int i = 0; i < 85; i++)
        {
            yield return new WaitForSeconds(attackTime);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z += + 1f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
        StartCoroutine("animIdle");
    }
}
