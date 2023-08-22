using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [Header("Properties")]
    public float speed;
    public float damage;
    public float bulletRange;

    [Header("Rocket")]
    public bool isRocket;
    public float sizeOfAOE;
    public float AOEDamage;

    [Header("Effects")]
    public ParticleSystem explode;
    public ParticleSystem trail;
    public AudioSource collide;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PolygonCollider2D c2D;
    private FadeInOut fadeInOut;
    private Vector3 startPosition;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2D = GetComponent<PolygonCollider2D>();
        fadeInOut = this.gameObject.GetComponent<FadeInOut>();

        sr.enabled = true;
        c2D.enabled = false;
        StartCoroutine("debug");
        StartCoroutine("bulletRangeWait");
    }

    void OnDrawGizmos()
    {
        if (isRocket)
        {
            // Set the color for the circle (you can choose any color you prefer)
            Gizmos.color = Color.red;

            // Draw the circle using the transform position of the GameObject
            Gizmos.DrawWireSphere(transform.position, sizeOfAOE);
        }
    }

    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        sr.enabled = false;
        c2D.enabled = false;
        if (isRocket)
        {
            TriggerRocketAOEDamage();
        }
        trail.Stop();
        explode.Play();
        collide.Play();
        StartCoroutine("destroyThis");
    }

    void TriggerRocketAOEDamage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), sizeOfAOE);
        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.gameObject.tag == "Enemy")
            {
                EnemyHealthManager enemyHealthManager = collider2D.gameObject.GetComponent<EnemyHealthManager>();
                enemyHealthManager.enemyHealth -= AOEDamage;
                enemyHealthManager.alphaValue = 1;
            }
        }
    }

    IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    IEnumerator bulletRangeWait()
    {
        startPosition = transform.position;
        while (Vector3.Distance(startPosition, transform.position) <= bulletRange)
        {
            yield return null;
        }
        fadeInOut.FadeOutObject();
    }

    IEnumerator debug()
    {
        yield return new WaitForSeconds(0.2f);
        c2D.enabled = true;
    }
}
