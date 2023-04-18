using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public static class SaveManager 
{
    public static SaveData CurrentSaveData = new SaveData();
    public const string saveDirectory = "/savedData/";
    public const string Filename = "SaveGame.txt";
    //Handles saving the game
    public static bool Save()
    {
        var dir = Application.persistentDataPath + saveDirectory;
        //If the directory to save the game doesn't exist, this creates it before saving to it.
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(CurrentSaveData, true);
        File.WriteAllText(dir + Filename, json);

        GUIUtility.systemCopyBuffer = dir;
        return true;
    }

    public static void LoadGame()
    {
        //Saves the fulls path including the name of the file I'm loading
        string fullPath = Application.persistentDataPath + saveDirectory + Filename;
        SaveData temp = new SaveData();
        //Checks if the file for the saved game exists when trying to load it
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            temp = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("Save doesn't exist!");
        }

        CurrentSaveData = temp;
    } 

    public static void NewGame()
    {
        var dir = Application.persistentDataPath + saveDirectory;
        if (Directory.Exists(dir))
        {
            string json = JsonUtility.ToJson(CurrentSaveData.playerSaveData.CurrentScene, true);
            File.WriteAllText(dir + Filename, json);
        }
    }
}
