using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //Variables and calling other scripts, getting different componenets 
    public DetectionZone edgeZone;
    public DetectionZone attackZone;
    spaceTouchDirection touchDirection;
    public Transform player;
    takeDamage damage;
    Animator animator;
    Rigidbody2D rb;
    public float _walkSpeed;
    private bool _seesTarget = false;
    float currentDist;
    public float targetRange = 5f;
    
    //saves the moving direction
    public enum moveDirection { Right, Left }
    //variables to handle movement direction
    private moveDirection _moveDirection;
    private Vector2 moveDirectionV = Vector2.right;
    private float stopRate = 0.02f;
    public bool moveAllowed
    {
        get
        {
            return animator.GetBool(AnimStrings.moveAllowed);
        }

    }
    public float walkSpeed
    {
        get { return _walkSpeed; }
        
    }

    public moveDirection walkDirection
    {
        get { return _moveDirection; }
        set
        {
            if (_moveDirection != value)
            {
                //flips direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == moveDirection.Right)
                {
                    moveDirectionV = Vector2.right;
                }
                else if (value == moveDirection.Left)
                {
                    moveDirectionV = Vector2.left;
                }
            }
            _moveDirection = value;
        }
    }
    public bool seesTarget
    {
        get
        {
            return _seesTarget;
        }
        set
        {
            _seesTarget = value;
            animator.SetBool(AnimStrings.seeTarget, value);
        }
    }

    public float AttackCooldown 
    { 
        get 
        {
           return animator.GetFloat(AnimStrings.attackCooldown);
        } 
        private set
        {
            animator.SetFloat(AnimStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchDirection = GetComponent<spaceTouchDirection>();
        animator = GetComponent<Animator>();
        damage = GetComponent<takeDamage>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        
    }
    // Update is called once per frame
    private void Update()
    {
        seesTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

    }
    void FixedUpdate()
    {

        
        currentDist = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(currentDist);
        
        
        if (touchDirection.isGrounded && touchDirection.isOnWall || edgeZone.detectedColliders.Count == 0)
        {
            ChangeDirection();
        }
        if (!damage.LockVelocity)
        {
            if (moveAllowed)
            {
                rb.velocity = new Vector2(walkSpeed * moveDirectionV.x, rb.velocity.y);
                if (currentDist <= targetRange)
                {
                    if (player.position.x > transform.position.x)
                    {
                        walkDirection = moveDirection.Right;
                    }
                    else if (player.position.x < transform.position.x)
                    {
                        walkDirection = moveDirection.Left;
                    }
                    transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, transform.position.y), walkSpeed * Time.deltaTime);

                }
            }
            else //need to add in that stops following when attacking player
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopRate), rb.velocity.y);
            }
        }
        
    }
    public void ChangeDirection()
    {
        if (walkDirection == moveDirection.Right)
        {
            walkDirection = moveDirection.Left;
        }
        else if (walkDirection == moveDirection.Left)
        {
            walkDirection = moveDirection.Right;
        }
        else
        {
            Debug.LogError("Invalid walking direction");
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
