using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [Header("Properties")]
    public float speed;
    public float damage;
    public float bulletRange;

    private Rigidbody2D rb;
    [SerializeField] private ParticleSystem explode;
    [SerializeField] private ParticleSystem trail;
    private SpriteRenderer sr;
    private PolygonCollider2D c2D;
    public AudioSource collide;
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

    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        sr.enabled = false;
        c2D.enabled = false;
        trail.Stop();
        explode.Play();
        collide.Play();
        StartCoroutine("destroyThis");
    }

    IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(0.12f);
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

    //IEnumerator delag()
    //{
    //    
    //    yield return new WaitForSeconds(timeUntilDestroy);
    //    fadeInOut.FadeOutObject();
    //    //Destroy(this.gameObject);
    //}

    IEnumerator debug()
    {
        yield return new WaitForSeconds(0.2f);
        c2D.enabled = true;
    }
}
