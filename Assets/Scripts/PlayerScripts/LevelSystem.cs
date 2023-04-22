using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    TMP_Text lvlText;
    public static LevelSystem instance;
    [SerializeField]
    private int currentXP;
    [SerializeField]
    private int CurrentLevel;
    private int xpToNextLevel;
    public int CurrentXP
    {
        get { return currentXP; }
        set { currentXP = value; }
    }
    public int PlayerLevel
    {
        get { return CurrentLevel; }
        set { CurrentLevel = value; }
    }
    private void Awake()
    {
        //setting the player's starting level
        CurrentLevel = 0;
        
       lvlText = GameObject.Find("PlayerLevel").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        lvlText.text = "Level: " + CurrentLevel;
    }
    //updates the player XP whenever the player recieves new XP whether through quests or killing enemies
    public void updateXP(int exp)
    {
        currentXP += exp;
        //finds the player's gameobject and the takedamage script attached to it inorder to change its varibles as needed
        var stats = GameObject.Find("Player").GetComponent<damageManager>();
        
        int cLvl = (int)(0.1f * Mathf.Sqrt(currentXP));

        if (cLvl != CurrentLevel)
        {
            CurrentLevel = cLvl;
            stats.CurrentHealth = stats.MaxHP; //The player regains health upon leveling up
            //For the sake of the fact that this is a prototype, the player's max health increases by 5 every 2 levels
            if (CurrentLevel % 2 == 0)
            {
                stats.MaxHP += 5;
            }
            UpdateLevelText(cLvl);
        }

        xpToNextLevel = (int)(50f * (Mathf.Pow(CurrentLevel + 1, 2) - (5 * (CurrentLevel + 1)) + 8));
        int diffXP = xpToNextLevel - currentXP;

        int totalDiff = xpToNextLevel - (100 * CurrentLevel * CurrentLevel);
    }
    public void UpdateLevelText(int level)
    {
        lvlText.text = "Level: " + level;
    }
}
