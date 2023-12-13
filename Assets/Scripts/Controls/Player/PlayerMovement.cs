using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 speed;

    [SerializeField]
    private Transform ArmSwitch;
    private Animator anim;
    private string currentState;
    private int facing;

    private KeycodeDatabase keycodeDatabase;
    private Dictionary<int, KeyCode> keybinds;

    private PlayerController playerController;
    public bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        playerController = GetComponent<PlayerController>();
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    void FixedUpdate()
    {
        if (keycodeDatabase == null)
        {
            keycodeDatabase = GameObject.Find("Player").GetComponent<KeycodeDatabase>();
        }
        this.keybinds = keycodeDatabase.GetFullDictionary();
        if (!keybinds.TryGetValue(0, out KeyCode keyCode))
        {
            Debug.Log("Player movement dictionary null. Initializing dicionary with default values");
            keybinds[0] = KeyCode.W;
            keybinds[1] = KeyCode.S;
            keybinds[2] = KeyCode.A;
            keybinds[3] = KeyCode.D;
        }

        if (Input.GetKey(keybinds[3]) || Input.GetKey(keybinds[2]) || Input.GetKey(keybinds[0]) || Input.GetKey(keybinds[1]))
        {
            //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed.x, Input.GetAxisRaw("Vertical") * speed.y);
            if (canMove == true)
            {
                if (Input.GetKey(keybinds[3]))
                {
                    rb.velocity = new Vector2(1 * speed.x, rb.velocity.y);
                }
                if (Input.GetKey(keybinds[2]))
                {
                    rb.velocity = new Vector2(-1 * speed.x, rb.velocity.y);
                }
                if (Input.GetKey(keybinds[1]))
                {
                    rb.velocity = new Vector2(rb.velocity.x, -1 * speed.y);
                }
                if (Input.GetKey(keybinds[0]))
                {
                    rb.velocity = new Vector2(rb.velocity.x, 1 * speed.y);
                }
            

                if (Input.GetKey(keybinds[2]) == false && Input.GetKey(keybinds[3]) == false)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            
                if (Input.GetKey(keybinds[0]) == false && Input.GetKey(keybinds[1]) == false)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && playerController.PlayerEnergy > 0)
        {
            playerController.sheildOn = true;
            canMove = false;
        }
        else
        {
            playerController.sheildOn = false;
            canMove = true;
        }

        this.keybinds = keycodeDatabase.GetFullDictionary();
        if (Input.GetKey(keybinds[1]) && canMove == true)
        {
            ChangeAnimationState("Down-Walking");
            facing = 1;
            ArmSwitch.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKey(keybinds[0]) && canMove == true)
        {
            ChangeAnimationState("Up-Walking");
            facing = 2;
            ArmSwitch.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetKey(keybinds[2]) && canMove == true)
        {
            ChangeAnimationState("Left-Walking");
            facing = 3;
            ArmSwitch.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKey(keybinds[3]) && canMove == true)
        {
            ChangeAnimationState("Right-Walking");
            facing = 4;
            ArmSwitch.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            if (facing == 1)
            {
                ChangeAnimationState("Forward");
            }
            else if (facing == 2)
            {
                ChangeAnimationState("Up");
            }
            else if (facing == 3)
            {
                ChangeAnimationState("Left");
            }
            else if (facing == 4)
            {
                ChangeAnimationState("Right");
            }
            else
            {
                ChangeAnimationState("Forward");
            }
        }
    }
}
