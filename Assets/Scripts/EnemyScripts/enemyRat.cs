using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRat : MonoBehaviour
{
    takeDamage damageable;
    Animator animator;
    [SerializeField]
    private int _hitPower = 10;
    [SerializeField]
    private int _maxHP = 80;
    [SerializeField]
    private int _health;
    [SerializeField]
    private bool _isAlive = true;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

}
