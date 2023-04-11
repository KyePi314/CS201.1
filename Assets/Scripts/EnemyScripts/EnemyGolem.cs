using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolem : MonoBehaviour
{
    takeDamage damageable;
    Animator animator;
    [SerializeField]
    private int _hitPower = 15;
    [SerializeField]
    private int _maxHP = 110;
    [SerializeField]
    private int _health;
    [SerializeField]
    private bool _isAlive = true;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
