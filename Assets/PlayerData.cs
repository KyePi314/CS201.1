using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SceneLoaderManager sceneLoaderManager;
    Transform player;
    damageManager playerStats;
    LevelSystem levelSystem;
    Vector2 spawnPos;
    private PlayerSaveData playerSave = new PlayerSaveData();
    
    private void Awake()
    {   
        sceneLoaderManager = GameObject.Find("GameManager").GetComponent<SceneLoaderManager>();
        playerStats = GameObject.Find("Player").GetComponent<damageManager>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        levelSystem = GameObject.Find("Player").GetComponent<LevelSystem>();
        spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            spawnPos = new Vector2(-6.14f, -2.20f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //if the player dies, they are sent back to their last save point
            if (!playerStats.IsAlive)
            {
                LoadPlayerData();
            }
        }

    }
    //Saves the player's data
    public void SavePlayerData()
    {
        playerSave.playerPos = player.transform.position;
        playerSave.MaxHP = playerStats.MaxHP;
        playerSave.CurrentHealth = playerStats.CurrentHealth;
        playerSave.XP = levelSystem.CurrentXP;
        playerSave.level = levelSystem.PlayerLevel;
        playerSave.CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SaveManager.CurrentSaveData.playerSaveData = playerSave;
        SaveManager.Save();
    }
    //Loads the player's data
    public void LoadPlayerData()
    {
        //Disables the sceneloadermanager as it was overriding the player spawn pos
        sceneLoaderManager.enabled = false;
        Debug.Log(playerSave.CurrentScene);
        SaveManager.LoadGame();
        playerSave = SaveManager.CurrentSaveData.playerSaveData;
        SceneLoad(playerSave.CurrentScene);
        playerStats.CurrentHealth = playerSave.CurrentHealth;
        playerStats.MaxHP = playerSave.MaxHP;
        levelSystem.CurrentXP = playerSave.XP;
        levelSystem.PlayerLevel = playerSave.level;
       
        playerStats.IsAlive = true;    }
    //Loads the scene which holds the last save point
    public void SceneLoad(int scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }
    //Waits until the scene has loaded and has found the save point before loading
    IEnumerator LoadSceneAsync(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene); 
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //If the player is starting a new game they'll have no set position so this puts them at the default
        if (SaveManager.CurrentSaveData.playerSaveData.CurrentScene == 0)
        {
            spriteRenderer.enabled = true;
            player.transform.position = spawnPos;
        }
        //If the player is loading a game, this sets them at their saved position
        else
        {
            player.transform.position = playerSave.playerPos;
        }
        //Once the level is loaded the player's sprite is enabled and the sceneloadermanager is enabled again
        if (asyncLoad.isDone)
        {
            spriteRenderer.enabled = true;
            sceneLoaderManager.enabled = true;
        }
    }

}
[System.Serializable]
public struct PlayerSaveData
{
    public int MaxHP;
    public int CurrentHealth;
    public int XP;
    public int level;
    public Vector2 playerPos;
    public int CurrentScene;
    //add a public int killCount?
}