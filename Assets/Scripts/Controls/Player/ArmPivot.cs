using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPivot : MonoBehaviour
{
    public GameObject player;
    public bool isMainArm;
    private float rotationZ2;
    private bool goingDown;

    [SerializeField]
    private SpriteRenderer sp;

    public bool isFlipped;

    void Start()
    {
        rotationZ2 = 85f;
        goingDown = true;
    }

    void FixedUpdate()
    {
        if (isMainArm == true)
        {
            sp.flipY = false;
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

            if (rotationZ < -90 || rotationZ > 90)
            {
                isFlipped = true;
                if (player.transform.eulerAngles.y == 0)
                {
                    transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
                }
                else if (player.transform.eulerAngles.y == 180)
                {
                    transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
                }
            }
            else
            {
                isFlipped = false;
            }
        }
        else if (isMainArm == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationZ2);

            if (player.transform.rotation != Quaternion.Euler(0f, 180f, 0f))
            {
                if (rotationZ2 < 70f || rotationZ2 > 85f)
                {
                    rotationZ2 = 85f;
                }

                sp.flipY = false;
                if (rotationZ2 == 85f)
                {
                    rotationZ2 -= 0.5f;
                    goingDown = true;
                }
                else if (rotationZ2 == 70f)
                {
                    rotationZ2 += 0.5f;
                    goingDown = false;
                }
                else if (rotationZ2 < 85f && goingDown == true)
                {
                    rotationZ2 -= 0.5f;
                }
                else if (rotationZ2 > 70f && goingDown == false)
                {
                    rotationZ2 += 0.5f;
                }
            }
            else
            {
                if (rotationZ2 < 85f || rotationZ2 > 100f)
                {
                    rotationZ2 = 100f;
                }

                sp.flipY = true;
                if (rotationZ2 == 100f)
                {
                    rotationZ2 -= 0.5f;
                    goingDown = true;
                }
                else if (rotationZ2 == 85f)
                {
                    rotationZ2 += 0.5f;
                    goingDown = false;
                }
                else if (rotationZ2 < 100f && goingDown == true)
                {
                    rotationZ2 -= 0.5f;
                }
                else if (rotationZ2 > 85f && goingDown == false)
                {
                    rotationZ2 += 0.5f;
                }
            }
        }
    }
}
