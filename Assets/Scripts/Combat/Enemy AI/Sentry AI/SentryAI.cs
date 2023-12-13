using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class SentryAI : MonoBehaviour
{
    //patrol / seePlayer Condition
    private bool seePlayer;

    //patrol system varriables
    public Vector3[] patrolPoints;
    public float patrolSpeed;
    public float patrolWaitTime;

    private int currentPointIndex;
    private bool patrolOnce;

    //Raycast vision
    public float visionRaycastDistance;

    public Transform raycastPivot;
    public Transform drawLineTarget;
    public Transform raycastPivot2;
    public Transform drawLineTarget2;
    public Transform raycastPivot3;
    public Transform drawLineTarget3;
    public Transform raycastPivot4;
    public Transform drawLineTarget4;
    public Transform raycastPivot5;
    public Transform drawLineTarget5;

    private float rotationZ;


    //Pathfinding
    private AIPath pathfinding;
    private bool pathOnce;

    //Animator
    private Animator anim;

    //Arms
    public GameObject leftArm;
    private SentryAI_Arms leftScr;
    public GameObject rightArm;
    private SentryAI_Arms rightScr;

    //Player
    private GameObject player;
    private Transform playerT;

    //Shooting
    public GameObject bullet;
    public float shootRate;
    private bool shootOnce;
    
    public Transform leftShootPoint;
    public Transform rightShootPoint;

    //Health
    public Slider healthbar;
    private EnemyHealthManager enemyHealthManager;
    public CanvasGroup fade;
    private bool fadeActive;

    //Death
    public GameObject DeadedPart;

    //Canvas
    public Canvas canvas;
    private GameObject mainCameraOb;
    private Camera mainCamera;

    void Awake()
    {
        pathfinding = GetComponent<AIPath>();
        anim = GetComponent<Animator>();

        leftScr = leftArm.GetComponent<SentryAI_Arms>();
        rightScr = rightArm.GetComponent<SentryAI_Arms>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.GetComponent<Transform>();

        mainCameraOb = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera = mainCameraOb.GetComponent<Camera>();
        canvas.worldCamera = mainCamera;

        enemyHealthManager = GetComponent<EnemyHealthManager>();
        enemyHealthManager.enemyHealth = 20f;
    }

    void Start()
    {
        patrolOnce = false;
        currentPointIndex = 0;
        pathfinding.enabled = false;
        enemyHealthManager.alphaValue = 0f;

        //raycast pivot setup
        drawLineTarget.position = new Vector3(visionRaycastDistance, 0, 0);
        drawLineTarget2.position = new Vector3(visionRaycastDistance, 0, 0);
        drawLineTarget3.position = new Vector3(visionRaycastDistance, 0, 0);
        drawLineTarget4.position = new Vector3(visionRaycastDistance, 0, 0);
        drawLineTarget5.position = new Vector3(visionRaycastDistance, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "PlayerBullet")
        {
            LaserBullet lb = other.gameObject.GetComponent<LaserBullet>();
            enemyHealthManager.enemyHealth -= lb.damage;

            seePlayer = true;
            enemyHealthManager.alphaValue = 1f;
            StartCoroutine("HealthFade");
        }
    }

    void FixedUpdate()
    {
        // checks and updates
        healthbar.value = enemyHealthManager.enemyHealth;
        fade.alpha = enemyHealthManager.alphaValue;

        if (enemyHealthManager.alphaValue == 1 && fadeActive == false)
        {
            StartCoroutine("HealthFade");
        }

        if (enemyHealthManager.enemyHealth <= 0)
        {
            Death();
        }

        //Patrol and search functions
        if (seePlayer == false)
        {
            pathfinding.enabled = false;

            //raycast pivot
            if (transform.position != patrolPoints[currentPointIndex])
            {
                Vector3 difference = patrolPoints[currentPointIndex] - transform.position;
                difference.Normalize();
                rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                raycastPivot.rotation = Quaternion.Euler(0, 0, rotationZ);
                raycastPivot2.rotation = Quaternion.Euler(0, 0, rotationZ + 10f);
                raycastPivot3.rotation = Quaternion.Euler(0, 0, rotationZ + 20f);
                raycastPivot4.rotation = Quaternion.Euler(0, 0, rotationZ - 20f);
                raycastPivot5.rotation = Quaternion.Euler(0, 0, rotationZ - 10f);
            }
            else
            {
                raycastPivot.rotation = Quaternion.Euler(0, 0, rotationZ);
            }

            //raycast calculations 
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, raycastPivot.right, visionRaycastDistance);

            if (raycast.collider != null)
            {
                Debug.DrawLine(transform.position, raycast.point, Color.red);
                if (raycast.collider.tag == "Player")
                {
                    seePlayer = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, drawLineTarget.position, Color.green);
            }

            //raycast calculations 2
            RaycastHit2D raycast2 = Physics2D.Raycast(transform.position, raycastPivot2.right, visionRaycastDistance);

            if (raycast2.collider != null)
            {
                Debug.DrawLine(transform.position, raycast2.point, Color.red);
                if (raycast2.collider.tag == "Player")
                {
                    seePlayer = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, drawLineTarget2.position, Color.green);
            }

            //raycast calculations 3
            RaycastHit2D raycast3 = Physics2D.Raycast(transform.position, raycastPivot3.right, visionRaycastDistance);

            if (raycast3.collider != null)
            {
                Debug.DrawLine(transform.position, raycast3.point, Color.red);
                if (raycast3.collider.tag == "Player")
                {
                    seePlayer = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, drawLineTarget3.position, Color.green);
            }

            //raycast calculations 4
            RaycastHit2D raycast4 = Physics2D.Raycast(transform.position, raycastPivot4.right, visionRaycastDistance);

            if (raycast4.collider != null)
            {
                Debug.DrawLine(transform.position, raycast4.point, Color.red);
                if (raycast4.collider.tag == "Player")
                {
                    seePlayer = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, drawLineTarget4.position, Color.green);
            }

            //raycast calculations 5
            RaycastHit2D raycast5 = Physics2D.Raycast(transform.position, raycastPivot5.right, visionRaycastDistance);

            if (raycast5.collider != null)
            {
                Debug.DrawLine(transform.position, raycast5.point, Color.red);
                if (raycast5.collider.tag == "Player")
                {
                    seePlayer = true;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, drawLineTarget5.position, Color.green);
            }

            //patrol system
            if (enemyHealthManager.stunned == false)
            {
                if (transform.position != patrolPoints[currentPointIndex])
                {
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex], patrolSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    if (patrolOnce == false)
                    {
                        patrolOnce = true;
                        if (currentPointIndex + 1 < patrolPoints.Length)
                        {
                            changePatrolAnimation(patrolPoints[currentPointIndex + 1], false);
                        }
                        else
                        {
                            changePatrolAnimation(patrolPoints[0], false);
                        }
                        StartCoroutine("patrolWait");
                    }  
                }
            }
        }
        else if (seePlayer == true)
        {
            anim.Play("Down-Walking");
            if (enemyHealthManager.stunned == false)
            {
                pathfinding.enabled = true;
            }
            else
            {
                pathfinding.enabled = false;
            }
            
            if (playerT.position.x < transform.position.x)
            {
                leftScr.isActive = true;
                rightScr.isActive = false;
            }
            else if (playerT.position.x >= transform.position.x)
            {
                leftScr.isActive = false;
                rightScr.isActive = true;
            }

            //Pathfinding animations / raycast
            float distance2 = Vector3.Distance(transform.position, playerT.position);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, (playerT.position - transform.position), distance2);
            Debug.DrawRay(transform.position, (playerT.position - transform.position), Color.magenta);
            if (raycastHit2D.collider.tag == "Player" && shootOnce == false && enemyHealthManager.stunned == false)
            {
                shootOnce = true;
                StartCoroutine("Shoot");
            }

            if (pathOnce == false)
            {
                pathOnce = true;
                StartCoroutine("changePathAnim");
            }
        }
    }

    IEnumerator HealthFade()
    {
        fadeActive = true;
        yield return new WaitForSeconds(8f);
        enemyHealthManager.alphaValue -= 0.01f;
        for (int i = 0; i < 99; i++)
        {
            if (enemyHealthManager.alphaValue == 1)
            {
                break;
            }
            yield return new WaitForSeconds(0.01f);
            enemyHealthManager.alphaValue -= 0.01f;
        }
        fadeActive = false;
    }

    IEnumerator Shoot()
    {
        if (playerT.position.x < transform.position.x)
        {
            Instantiate(bullet, leftShootPoint.position, leftShootPoint.rotation);
            yield return new WaitForSeconds(shootRate);
            shootOnce = false;
        }
        else if (playerT.position.x >= transform.position.x)
        {
            Instantiate(bullet, rightShootPoint.position, rightShootPoint.rotation);
            yield return new WaitForSeconds(shootRate);
            shootOnce = false;
        }
    }

    void changePatrolAnimation(Vector3 nextPosition, bool walking)
    {
        if (nextPosition.y > transform.position.y && walking == true)
        {
            anim.Play("Up-Walking");
        }
        else if (nextPosition.y < transform.position.y && walking == true)
        {
            anim.Play("Down-Walking");
        }
        else if (nextPosition.y > transform.position.y && walking == false)
        {
            anim.Play("Up");
        }
        else if (nextPosition.y < transform.position.y && walking == false)
        {
            anim.Play("Down");
        }
    }

    IEnumerator patrolWait()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        patrolOnce = false;
        changePatrolAnimation(patrolPoints[currentPointIndex], true);
    }

    IEnumerator changePathAnim()
    {
        Vector3 currentposition = transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 newposition = transform.position;
    }

    public void Death()
    {
        Instantiate(DeadedPart, transform.position, DeadedPart.transform.rotation);
        Destroy(this.gameObject);
    }

}
