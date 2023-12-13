using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAI_Arms : MonoBehaviour
{
    //Active
    public bool isActive;
    private bool checkIsActive;

    private float armVal;
    private bool goingDown;

    //Arm
    public string armSide;
    public Animator anim;

    //Player
    public GameObject player;

    void FixedUpdate()
    {
        //On varriable change (Animations)
        if (isActive != checkIsActive)
        {
            checkIsActive = isActive;
            if (isActive == true)
            {
                anim.Play(armSide + "Arm-Opening");
                StartCoroutine("setState", true);
            }
            else if (isActive == false)
            {
                anim.Play(armSide + "Arm-Hiding");
                StartCoroutine("setState", false);
            }

        }

        //When Active point toward player
        if (isActive == true)
        {
            Vector3 difference = player.transform.position - transform.position;
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (armSide == "Right")
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            }
            else if (armSide == "Left")
            {
                transform.rotation = Quaternion.Euler(180f, 180f, rotationZ);
            }
        }

        if (isActive == false)
        {
            if (armSide == "Right")
            {
                if (armVal > -70f || armVal < -85f)
                {
                    int intFloat;
                    intFloat = System.Convert.ToInt32(armVal);
                    armVal = intFloat;

                    if (armVal > -70f)
                    {
                        armVal -= 1;
                    }
                    else if (armVal < -85f)
                    {
                        armVal += 1;
                    }
                }
                else if (armVal <= -70f && armVal >= -85f)
                {
                    if (armVal == -70f)
                    {
                        goingDown = true;
                    }
                   else if (armVal == -85f)
                    {
                        goingDown = false;
                    }

                    if (goingDown == true)
                    {
                        armVal -= 0.5f;
                    }
                    else if (goingDown == false)
                    {
                        armVal += 0.5f;
                    }
                }
                transform.rotation = Quaternion.Euler(0, 0, armVal);
            }
            else if (armSide == "Left")
            {
                if (armVal < 70f || armVal > 85f)
                {
                    int intFloat;
                    intFloat = System.Convert.ToInt32(armVal);
                    armVal = intFloat;

                    if (armVal < 70f)
                    {
                        armVal += 1;
                    }
                    else if (armVal > 85f)
                    {
                        armVal -= 1;
                    }
                }
                else if (armVal >= 70f && armVal <= 85f)
                {
                    if (armVal == 70f)
                    {
                        goingDown = false;
                    }
                   else if (armVal == 85f)
                    {
                        goingDown = true;
                    }

                    if (goingDown == true)
                    {
                        armVal -= 0.5f;
                    }
                    else if (goingDown == false)
                    {
                        armVal += 0.5f;
                    }
                }
                transform.rotation = Quaternion.Euler(0, 0, armVal);
            }
        }
    }

    IEnumerator setState(bool isOpening)
    {
        yield return new WaitForSeconds(1f);
        if (isOpening == true)
        {
            anim.Play(armSide + "Arm-Open");
        }
        if (isOpening == false)
        {
            anim.Play(armSide + "Arm-Hidden");
        }
    }
}
