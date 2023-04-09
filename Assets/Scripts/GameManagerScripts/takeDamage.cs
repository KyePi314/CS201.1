using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class takeDamage : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
   
    Animator animator;
    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _health;
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float lastHitTimer = 0;
    private float invincibiltyTimer = 0.25f;
    private int _hitPower;
    public int MaxHP
    {
        get
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;
        }
    }
    public int CurrentHealth
    {
        get
        {
            return _health;

        }
        set
        {
            _health = value;

        }
    }
    public int attackHit
    {
        get
        {
            return _hitPower;
        }
        set
        {

            _hitPower = value;
        }
    }
    
    public bool IsAlive
    {
        get
        {

            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimStrings.isAlive, value);
        }
    }
   
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimStrings.lockVelocity, value);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();

        
    }
    public void Update()
    {
        if (isInvincible)
        {
            if (lastHitTimer > invincibiltyTimer)
            {
                isInvincible = false;
                lastHitTimer = 0;
            }

            lastHitTimer += Time.deltaTime;
        }
        
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            CurrentHealth -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimStrings.hitTrigger);
            LockVelocity = true;
            if (CurrentHealth <= 0)
            {
                IsAlive = false;
            }
           
            damageableHit?.Invoke(damage, knockback);
            return true;

        }
        else
        {
            return false;
        }
           
        
    }


}
