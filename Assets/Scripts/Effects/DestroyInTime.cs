using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    public float destroyInSeconds;

    void Start() {StartCoroutine(destroy());}

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(destroyInSeconds);
        Destroy(this.gameObject);
    }
}
