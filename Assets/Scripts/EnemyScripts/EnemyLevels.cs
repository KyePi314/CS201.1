using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevels : MonoBehaviour
{
    LevelSystem playerLvl;
    [SerializeField]
    public int enemyXP;
    public int enemyLevel;

    private void Awake()
    {
        playerLvl = GameObject.Find("Player").GetComponent<LevelSystem>();
    }
    // Update is called once per frame
    void Update()
    { 
        //For the prototype, enemies will scale up in level alongside the player by matching the player's level
        if (enemyLevel != playerLvl.PlayerLevel)
        {
            enemyLevel = playerLvl.PlayerLevel;
        }
        scaleXP();
        
    }
    //Scales up the XP that the enemy drops to the player on death by ten for each level they get to
    public void scaleXP()
    {
        enemyXP = (enemyLevel * 10) + 10;
    }
}
