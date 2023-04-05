using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRat : MonoBehaviour
{
    spaceTouchDirection touchDirection;
    public float walkSpeed = 3f;

    Rigidbody2D rb;

    public enum moveDirection { Right, Left }

    private moveDirection _moveDirection;
    private Vector2 moveDirectionV = Vector2.right;

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
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchDirection.isGrounded && touchDirection.isOnWall)
        {
            ChangeDirection();
        }    
        rb.velocity = new Vector2(walkSpeed * moveDirectionV.x, rb.velocity.y);
    }

    private void ChangeDirection()
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
