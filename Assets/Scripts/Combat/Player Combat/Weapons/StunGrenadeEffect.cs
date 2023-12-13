using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenadeEffect : MonoBehaviour
{
    public float radius;
    public float delay;

    // immeadiate effect
    void Start()
    {
        // find all objects in radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        // for each enemy collider affect them with stunned
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                EnemyHealthManager enemyHealthManager = collider.gameObject.GetComponent<EnemyHealthManager>();
                enemyHealthManager.stunned = true;
            }
        }
        // wait a little
        StartCoroutine("wait");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(delay);
        // find all objects in radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        // for each enemy collider remove the stunned effect
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                EnemyHealthManager enemyHealthManager = collider.gameObject.GetComponent<EnemyHealthManager>();
                enemyHealthManager.stunned = false;
            }
        }
        Destroy(this.gameObject);
    }
}
