using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class takeDamage : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    enemyHealthandDamage enemy;
    PlayerHealthandDamage player;
    Animator animator;
    //[SerializeField]
    //private int _maxHealth;
    //[SerializeField]
    //private int _health;
    //[SerializeField]
    //private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float lastHitTimer = 0;
    private float invincibiltyTimer = 0.25f;
    

    //public int maxHealth
    //{
    //    get
    //    {
    //        return _maxHealth;
    //    }
    //    set
    //    {
    //        _maxHealth = value;
    //    }
    //}

    //public int Health
    //{
    //    get
    //    {
    //        return _health;

    //    }
    //    set
    //    {
    //        _health = value;
            
    //    }
    //}

    //public bool IsAlive
    //{
    //    get
    //    {
            
    //        return _isAlive;
    //    }
    //    set
    //    {
    //        _isAlive = value;
    //        animator.SetBool(AnimStrings.isAlive, value);
    //        Debug.Log("IsAlive set " + value);
    //    }
    //}

    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimStrings.isHit, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        player = GetComponent<PlayerHealthandDamage>();
        enemy =  GetComponent<enemyHealthandDamage>();
        
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

        if (gameObject.CompareTag("Enemy"))
        {
            damage = enemy.attackHit;
            if (enemy.IsAlive && !isInvincible)
            {
                enemy.Health -= damage;
                isInvincible = true;

                if (enemy.Health <= 0)
                {
                    enemy.IsAlive = false;
                }
                IsHit = true;
                damageableHit?.Invoke(damage, knockback);
                return true;

            }
            else
            {
                return false;
            }
        }

        else if (gameObject.CompareTag("Player"))
        {
            if (player.IsAlive && !isInvincible)
            {
                player.Health -= damage;
                isInvincible = true;

                if (player.Health <= 0)
                {
                    player.IsAlive = false;
                }
                IsHit = true;
                damageableHit?.Invoke(damage, knockback);
                return true;

            }
            else
            {
                return false;
            }
        }
        else { return false; }
        
    }


}
