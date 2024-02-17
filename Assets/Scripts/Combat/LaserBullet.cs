using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
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
    private EventInstance rocketSustain;
    public GameObject rocketExplosion;

    [Header("Effects")]
    public ParticleSystem explode;
    public ParticleSystem trail;

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

        if (!isRocket) {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.laserShoot, this.transform.position);
        }

        sr.enabled = true;
        c2D.enabled = false;
        StartCoroutine("debug");
        StartCoroutine("bulletRangeWait");
    }

    void Start()
    {
        rocketSustain = AudioManager.instance.CreateEventInstance(FMODEvents.instance.rocketSustain);
        if (isRocket) {
            rocketSustain.start();
        }
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
        rb.velocity = new Vector2(0,0);
        sr.enabled = false;
        c2D.enabled = false;

        StopCoroutine(bulletRangeWait());

        if (isRocket)
        {
            TriggerRocketAOEDamage();
            rocketSustain.stop(STOP_MODE.ALLOWFADEOUT);
        }
        trail.Stop();
        StartCoroutine(destroyThis());
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

            if (collider2D.gameObject.tag == "Player")
            {
                PlayerController playerController = collider2D.gameObject.GetComponent<PlayerController>();
            }
        }
    }

    IEnumerator destroyThis()
    {
        if (!isRocket)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.laserCollide, this.transform.position);
            if (explode != null) {explode.Play();}
            yield return new WaitForSeconds(2);
            Destroy(this.gameObject);
        }
        else if (isRocket)
        {
            Instantiate(rocketExplosion, transform.position, Quaternion.Euler(0,0,0));
            AudioManager.instance.PlayOneShot(FMODEvents.instance.rocketExplode, this.transform.position);
            Destroy(this.gameObject);
        }
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
