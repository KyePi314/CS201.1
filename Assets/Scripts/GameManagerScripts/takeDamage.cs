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
    public float timer = 0.5f;

    private float timeElapsed = 0;
    LevelSystem levelSystem;
    EnemyLevels enemyLevels;
    SpriteRenderer spriteRenderer;
    GameObject objRemoved;
    Color startColor;
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
        levelSystem = GameObject.Find("Player").GetComponent<LevelSystem>();
        enemyLevels = GetComponent<EnemyLevels>();
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
        if (!IsAlive && gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Player got XP " + enemyLevels.enemyXP);
            timeElapsed = 0f;
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            startColor = spriteRenderer.color;
            objRemoved = animator.gameObject;

            timeElapsed += Time.deltaTime;
            float newAlpha = startColor.a * (1 - (timeElapsed / timer));
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if (timeElapsed > timer)
            {
                Destroy(objRemoved);
            }
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
                if (gameObject.tag.Equals("Enemy"))
                {
                    levelSystem.updateXP(enemyLevels.enemyXP);
                    
                }
                
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
