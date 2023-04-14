using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager 
{
    public static SaveData CurrentSaveData = new SaveData();
    public const string saveDirectory = "/savedData/";
    public const string Filename = "SaveGame.sav";
    public static bool Save()
    {
        var dir = Application.persistentDataPath + saveDirectory;
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(CurrentSaveData, true);
        File.WriteAllText(dir + Filename, json);

        GUIUtility.systemCopyBuffer = dir;
        return true;
    }
}
