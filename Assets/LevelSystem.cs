using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    [SerializeField]
    private int currentXP;
    [SerializeField]
    private int CurrentLevel;
    private int xpToNextLevel;
    private int baseXP;
    
    public int PlayerLevel
    {
        get { return CurrentLevel; }
    }
    
    private void Awake()
    {
        //setting the player's starting level
       CurrentLevel = 0;
    }
    //updates the player XP whenever the player recieves new XP whether through quests or killing enemies
    public void updateXP(int exp)
    {
        currentXP += exp;

        int cLvl = (int)(0.1f * Mathf.Sqrt(currentXP));

        if (cLvl != CurrentLevel)
        {
            CurrentLevel = cLvl;
            //new level message
        }

        xpToNextLevel = (int)(50f * (Mathf.Pow(CurrentLevel + 1, 2) - (5 * (CurrentLevel + 1)) + 8));
        int diffXP = xpToNextLevel - currentXP;

        int totalDiff = xpToNextLevel - (100 * CurrentLevel * CurrentLevel);

    }

}
