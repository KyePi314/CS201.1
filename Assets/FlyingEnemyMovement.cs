using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    public DetectionZone attackDetect;
    damageManager damage;
    Transform player;
    public List<Transform> waypoints;
    Animator animator;
    Rigidbody2D rb;
    Transform nextPoint;
    public float waypointReachDist = 0.2f;
    public int flySpeed = 3;
    private bool _seesTarget = false;
    private bool isStunned = false;
    private float stunnedTime = 0.2f;
    private float targetRange = 6;

    int wayPointNum = 0;

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
    public bool moveAllowed
    {
        get
        {
            return animator.GetBool(AnimStrings.moveAllowed);
        }

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damage = GetComponent<damageManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Start()
    {
        nextPoint = waypoints[wayPointNum];
    }
    private void Update()
    {
        seesTarget = attackDetect.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (damage.IsAlive)
        {
            if (moveAllowed && AttackCooldown <= 0.01f)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
        Vector2 directToWayPoint = (nextPoint.position - transform.position).normalized;
        float dist = Vector2.Distance(nextPoint.position, transform.position);
        rb.velocity = directToWayPoint * flySpeed;
        flightDirection();
        if (dist <= waypointReachDist)
        {
            wayPointNum++;

            if (wayPointNum >= waypoints.Count)
            {
                wayPointNum = 0;
            }

            nextPoint = waypoints[wayPointNum];
        }
        else if (playerDist <= targetRange)
            {

                if (Mathf.Abs(this.transform.position.y - player.transform.position.y) < 1f) //checking to make sure that they will only follow a player if they are on a similar y level as them, otherwise the enemy would continue to track a player even if they were above them if they were still in range.
                {

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, flySpeed * Time.deltaTime);
                }
            }
        
    }
    private void flightDirection()
    {
        Vector3 scale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(- 1 * scale.x, scale.y, scale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
            }
        }
    }
}
