using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler
{
    private int currentLevel;
    private int XP;
    private int xpToNextLevel;

    public LevelHandler()
    {
        currentLevel = 0;
        XP = 0;
        xpToNextLevel = 100;
    }

    public void AddXP(int exp)
    {
        XP += exp;
        if (XP >= xpToNextLevel)
        {
            currentLevel++;
            XP -= xpToNextLevel;
        }
    }

    public int LevelNumber()
    {
        return currentLevel;
    }
}
