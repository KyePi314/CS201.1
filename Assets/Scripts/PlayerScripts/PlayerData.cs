using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : ScriptableObject 
{

    damageManager damageable;
    Animator animator;
    
    

    ////public int maxHealth
    ////{
    ////    get
    ////    {
    ////        return _maxHealth;
    ////    }
    ////    set
    ////    {
    ////        _maxHealth = value;
    ////    }
    ////}

    ////public int Health
    ////{
    ////    get
    ////    {
    ////        return _health;

    ////    }
    ////    set
    ////    {
    ////        _health = value;

    ////    }
    ////}

    ////public bool IsAlive
    ////{
    ////    get
    ////    {

    ////        return _isAlive;
    ////    }
    ////    set
    ////    {
    ////        _isAlive = value;
    ////        animator.SetBool(AnimStrings.isAlive, value);
    ////        Debug.Log("IsAlive set " + value);
    ////    }
    ////}


    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //    damageable = GetComponent<takeDamage>();
    //}
}
