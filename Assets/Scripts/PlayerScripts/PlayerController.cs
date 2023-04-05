using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*This script handles the player's movements and how they react to different input
 * It handles the player's speed based off if they are idle, walking or running 
 * handles how the player moves
 * handles what direction they're facing
 * */
public class PlayerController : MonoBehaviour
{
    spaceTouchDirection touchDir;

    //Variables for player controller
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private bool _isMoving = false;
    private bool _isRunning = false;
    public bool _isFacedRight = true;
    public float jumpimpulse = 5f;
    public float airSpeed = 5f;
    private bool doubleJump;
    private int jumpCount;

    //Used to access the animator and RigidBody components for objects with the playercontroller script attached to them (eg the player)
    Rigidbody2D rb;
    Animator animator;

    //My getter and setters for managing the player movement, movement states and faced direction
    public bool IsMoving //Handles getting and setting whether or not the player is moving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimStrings.isMoving, value);
        }
    }
    
    public bool IsRunning //Handles the getting and setting on whether the player is running or not
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimStrings.isRunning, value);
        }
    }
    public float currentSpeed //Handles the getting and setting on what speed the player is going to go depending on the values that match the current move input state (walk or run)
    {
        get
        {
            if (moveAllowed)
            {
                if (IsMoving && !touchDir.isOnWall) //Checks if the player is moving, and if they are sets the correct speed for if they are running or walking.
                {
                    //if (touchDir.isGrounded)
                    //{
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    //}
                    //else
                    //{
                    //    return airSpeed;
                    //}
                }
                else //if the player is idle, the player speed is idle
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        }
    }
    //Used for getting what direction the player is facing, and handles the flipping of the character for which direction in which they face
    public bool IsFacedRight { get 
        { 
            return _isFacedRight;

        }
        private set 
        { 
            if (_isFacedRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacedRight = value;
        } 
    }

    public bool moveAllowed
    {
        get
        {
            return animator.GetBool(AnimStrings.moveAllowed);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDir = GetComponent<spaceTouchDirection>();

    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        //Sets how fast the player is moving based off which move state its in (run, walk etc)
        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
        if (touchDir.isGrounded) //Sets the bool that checks whether or not the player has already double jumped to false if they are on the ground
        {
            doubleJump = false;
        }
        animator.SetFloat(AnimStrings.yVelocity, rb.velocity.y);
    }

    public void onMove(InputAction.CallbackContext context) 
    {
        //If the player is moving, this method gets the input from the user and uses it to move the player according to this input
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        setFacedDirection(moveInput);
    }
    //Used with the getter and setter to handle which direction the player is facing based on which direction along the x axis is being inputted via the moveinput variable
    private void setFacedDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacedRight)
        {
            IsFacedRight = true;
        }
        else if (moveInput.x < 0 && IsFacedRight)
        {
            IsFacedRight = false;
        }
    }
    //Sets whether the player is running or not depending on whether the input button (shift left) is being pushed
    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    //Handles whether the player is jumping of not based on if they are currently pressing the space key
    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started && touchDir.isGrounded && moveAllowed) //if statement for the inital jump
        {
            animator.SetTrigger(AnimStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpimpulse);
            doubleJump = false;
        }
        if (context.started && doubleJump == false) //if the player has only jumped once, the double jump bool will be false meaning that they can double jump.
        {
            animator.SetTrigger(AnimStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpimpulse);
            doubleJump = true;
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchDir.isGrounded)
        {
            animator.SetTrigger(AnimStrings.attack);
        }
    }
}
