using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //Variables and calling other scripts, getting different componenets 
    public DetectionZone edgeZone;
    public DetectionZone attackZone;
    public DetectionZone stunZone;
    spaceTouchDirection touchDirection;
    public Transform player;
    damageManager damage;
    Animator animator;
    Rigidbody2D rb;

    private bool isStunned = false;
    private float stunnedTime = 0.2f;
    public float _walkSpeed;
    private bool _seesTarget = false;
    float currentDist;
    //The target range will vary enemy to enemy. Some enemy's may be 'blinder' than others. For example, something like a golem would have far better eyesight than the cave d
    public float targetRange;

    //saves the moving direction
    public enum moveDirection { Right, Left }
    //variables to handle movement direction
    private moveDirection _moveDirection;
    private Vector2 moveDirectionV = Vector2.right;
    private float stopRate = 0.02f;
    //Used to check whether or not the enemy is allowed to move in its current state
    public bool moveAllowed
    {
        get
        {
            return animator.GetBool(AnimStrings.moveAllowed);
        }

    }
    //this handles setting the default speed of the enemy
    public float walkSpeed
    {
        get { return _walkSpeed; }

    }
    public bool IsStunned
    {
        get
        {
            return isStunned;
        }
        set
        {
            isStunned = value;
            animator.SetBool(AnimStrings.isStunned, value);
        }
    }
    //handles the direction that the enemies are moving them, for example used when checking for walls and edges, and flips the enemy when they have encountered one
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
    //Used for checking whether or not the enemy sees the target, in this case being the player
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
    //sets a cooldown for between attacks so the enemy can't keep attacking and give the player no chance to counter or escape
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
        damage = GetComponent<damageManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }
    // Update is called once per frame
    private void Update()
    {

        seesTarget = attackZone.detectedColliders.Count > 0;
        if (stunZone.detectedColliders.Count > 0 && gameObject.tag.Equals("Enemy"))
        {
            enemyStunned();
        }
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (isStunned)
        {
            StartCoroutine(StunnedTime());
        }
    }
    void FixedUpdate()
    {
        //In fixedupdate is the code that handles the enemy's actual movement. this triggers them to move across their area, and if the player comes within a certain range, sets their movement to go towards said player
        currentDist = Vector2.Distance(transform.position, player.transform.position);

        if (touchDirection.isGrounded && touchDirection.isOnWall || edgeZone.detectedColliders.Count == 0)
        {
            ChangeDirection();
        }
        if (!damage.LockVelocity)
        {
            //This makes sure that the enemy only follows the player when the enemy is allowed to actually move, and that they have to wait for the cooldown while attacking before moving further towards the player
            if (moveAllowed && AttackCooldown == 0)
            {
                rb.velocity = new Vector2(walkSpeed * moveDirectionV.x, rb.velocity.y);

                if (currentDist <= targetRange)
                {
                    if (Mathf.Abs(this.transform.position.y - player.transform.position.y) < 0.5f) //checking to make sure that they will only follow a player if they are on a similar y level as them, otherwise the enemy would continue to track a player even if they were above them if they were still in range.
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
            }
            else
            {
                //when engaging in attacking the player, the enemy cannot move and will slide to a stop, rather than just stopping abruptly 
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopRate), rb.velocity.y);
            }
        }

    }
    //Stuns the enemy when the player jumps on their head
    public void enemyStunned()
    {
        if (isStunned)
        {
            return;
        }
        IsStunned = true;
        Debug.Log("Enemy" + isStunned.ToString());
        StartCoroutine(StunnedTime());
    }
    //Handles how long the enemy is stunned for.
    IEnumerator StunnedTime()
    {
        yield return new WaitForSeconds(1.5f);
        IsStunned = false;
    }

    //used in changing the direction that the enemy is facing
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
    //Adds a knockback when getting hit
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

}