using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthandDamage : MonoBehaviour
{
    takeDamage damageable;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private bool _isAlive = true;

    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public int Health
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
            Debug.Log("IsAlive set " + value);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<takeDamage>();
    }
}
