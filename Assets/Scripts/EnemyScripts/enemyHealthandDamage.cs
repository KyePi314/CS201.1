using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthandDamage : MonoBehaviour
{
    GameObject[] enemies;
    public GameObject type;
    private void Awake()
     {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            type = enemies[i];
           
        }
     }
}
