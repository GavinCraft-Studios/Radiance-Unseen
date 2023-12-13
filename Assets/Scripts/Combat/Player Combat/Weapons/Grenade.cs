using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Grenade : MonoBehaviour
{
    [Header("Grenade Refrences")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite deactiveSprite;
    private SpriteRenderer sr;
    private CapsuleCollider2D capsuleCollider2D;

    [Header("Grenade Settings")]
    public float grenadeCharge;
    [SerializeField] private float blastRadius;
    [SerializeField] private float damage;
    [SerializeField] private bool spawnPrefab;
    [SerializeField] private GameObject prefabToSpawn;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        capsuleCollider2D = this.gameObject.GetComponent<CapsuleCollider2D>();
        capsuleCollider2D.enabled = false;
        StartCoroutine("grenadeCountdown");
    }

    IEnumerator grenadeCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        capsuleCollider2D.enabled = true;
        yield return new WaitForSeconds(grenadeCharge);
        explode(); 
    }

    void explode()
    {
        // Log Explosion
        //Debug.Log("Grenade Exploded");

        // Find all objects in area
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        // For each collider check if it is an enemy
        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                // damage enemys
                EnemyHealthManager enemyHealthManager = collider.gameObject.GetComponent<EnemyHealthManager>();
                enemyHealthManager.enemyHealth -= damage;
                // update healthbar
                enemyHealthManager.alphaValue = 1f;
            }

            if (collider.gameObject.tag == "Player")
            {
                PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
                playerController.deductor(damage);
            }
        }

        // If Spawn Prefab = True spawn desired prefab
        if (spawnPrefab)
        {
            Instantiate(prefabToSpawn, transform.position, transform.rotation);
        }

        // destroy grenade
        Destroy(this.gameObject);
    }
}
