using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;


/*This script handles the player's movements and how they react to different input
 * It handles the player's speed based off if they are idle, walking or running 
 * handles how the player moves
 * handles what direction they're facing
 * */
public class PlayerController : MonoBehaviour
{
    UIinventory uiInv;
    InventoryScript inventory;
    spaceTouchDirection touchDir;
    damageManager damage;
    //Variables for player controller
    Vector2 moveInput;
    private int statValue;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private bool _isMoving = false;
    private bool _isRunning = false;
    public bool _isFacedRight = true;
    public float jumpimpulse = 5f;
    public float airSpeed = 5f;
    private bool doubleJump;
    private int currentItemName;
    //Used to access the animator and RigidBody components for objects with the playercontroller script attached to them (eg the player)
    Rigidbody2D rb;
    Animator animator;
    

    //My getter and setters for managing the player movement, movement states and faced direction
    public int CurrentItemName
    {
        get { return currentItemName; }
        set { currentItemName = value; }
    }
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
    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimStrings.isAlive);
        }
    }
    private void Awake()
    {
        uiInv = GameObject.Find("Inventory").GetComponent<UIinventory>();
        inventory = GetComponent<InventoryScript>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDir = GetComponent<spaceTouchDirection>();
        damage = GetComponent<damageManager>();
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if(!damage.LockVelocity)
        {
            //Sets how fast the player is moving based off which move state its in (run, walk etc)
            rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
        }
        
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
        if (isAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            setFacedDirection(moveInput);
        }
        
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

    public void onUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            for (int i = 0; i < inventory.PlayerItems.Count; i++)
            {
                if (inventory.PlayerItems[i].id == currentItemName)
                {
                    //For now, only the health items will have functions, and maybe the potion. 
                    foreach (var stat in inventory.PlayerItems[i].stats)
                    {
                        //Allows players to use healing items
                        if (stat.Key == "Regen")
                        {
                            statValue = stat.Value;
                            break;
                        }
                        else if (stat.Key == "moveSpeed")
                        {
                            //Set a timer for this at some point
                            statValue = stat.Value;
                            break;
                        }
                    }

                    Debug.Log("Done " + inventory.PlayerItems[i].title);
                }
            }
            itemEffects();
        }
        
        Debug.Log(CurrentItemName);
    }

    public void itemEffects()
    {
        //Adds the correct amount of regen to the player's health
        damage.CurrentHealth += statValue;

        //Makes sure that the correct item is being deleted from the player's inventory when it is used, and that the correct amount is being taken away, when the item amount equals zero, the item is removed from the inventory slot
        for (int i = 0; i < inventory.PlayerItems.Count; i++) 
        {
            var t = inventory.PlayerItems.Find(x => x.stats.ContainsValue(statValue));
            if (t == inventory.PlayerItems[i])
            {
                if (inventory.PlayerItems[i].itemAmount > 0)
                {
                    inventory.PlayerItems[i].itemAmount -= 1;
                    Debug.LogWarning(inventory.PlayerItems[i].itemAmount);
                    break;
                }
                else if (inventory.PlayerItems[i].itemAmount == 0)
                {
                    inventory.RemoveItem(t.id);
                    break;
                }

            }
           
        }
       

        
    }
    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    
}
