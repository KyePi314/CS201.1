using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(spaceTouchDirection))]
public class spaceTouchDirection : MonoBehaviour
{
    //Variables etc
    Animator animator;
    CapsuleCollider2D touchCol;

    public ContactFilter2D castFilter;

    public float groundDistance = 0.05f;
    public float wallCheckDistance = 0.2f;
    public float ceillingDistance = 0.05f;
    //bools
    private bool _isGrounded;
    private bool _isOnWall;
    private bool _isOnCeilling;
    private bool _isEdge;
    //Raycast arrays to use in checking whether the gameobject is grounded, on the wall, or on the ceilling
    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceillingHits = new RaycastHit2D[5];
    

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimStrings.isGrounded, value);
        }
    }
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimStrings.isOnWall, value);
        }
    }

    public bool isOnCeilling
    {
        get
        {
            return _isOnCeilling;
        }
        set
        {
            _isOnCeilling = value;
            animator.SetBool(AnimStrings.isOnCeilling, value);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        touchCol = GetComponent<CapsuleCollider2D>();
    }
    
    //Keeps checking to see if the gameobject is grounded, on a wall or the ceilling of a scene
    void FixedUpdate()
    {
        isGrounded = touchCol.Cast(Vector2.down, castFilter, groundHit, groundDistance) > 0;
        isOnWall = touchCol.Cast(wallCheckDirection, castFilter, wallHits, wallCheckDistance) > 0;
        isOnCeilling = touchCol.Cast(Vector2.up, castFilter, ceillingHits, ceillingDistance) > 0;
    }

    
}

