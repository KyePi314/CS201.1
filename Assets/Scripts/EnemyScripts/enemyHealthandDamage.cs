using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthandDamage : MonoBehaviour
{
    takeDamage damageable;
    Animator animator;
    [SerializeField]
    private int hitPower;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _health;
    [SerializeField]
    private bool _isAlive = true;
    public GameObject[] enemies;

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
    public int attackHit
    {
        get
        {
            return hitPower;
        }
        set
        {
            hitPower = value;
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
        damageable = GetComponent<takeDamage>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
