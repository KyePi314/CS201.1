using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRat : MonoBehaviour
{
    public DetectionZone edgeZone;
    public DetectionZone attackZone;
    spaceTouchDirection touchDirection;
    Animator animator;
    public float walkSpeed = 3f;
    private bool _seesTarget = false;
    private int maxHP = 80;
    private int hitPower = 10;
    Rigidbody2D rb;

    public enum moveDirection { Right, Left }

    private moveDirection _moveDirection;
    private Vector2 moveDirectionV = Vector2.right;
    private float stopRate = 0.02f;

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

    public int Health
    {
        get
        {
            return maxHP;
        }
    }
    public int attack
    {
        get { return hitPower; }
    }
    public bool moveAllowed
    {
        get
        {
            return animator.GetBool(AnimStrings.moveAllowed);
        }

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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchDirection = GetComponent<spaceTouchDirection>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        seesTarget = attackZone.detectedColliders.Count > 0;
    }
    void FixedUpdate()
    {
        if (touchDirection.isGrounded && touchDirection.isOnWall || edgeZone.detectedColliders.Count == 0)
        {
            ChangeDirection();
        }
        if (moveAllowed)
        {
            rb.velocity = new Vector2(walkSpeed * moveDirectionV.x, rb.velocity.y);
        }
       else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, stopRate), rb.velocity.y);
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


}
