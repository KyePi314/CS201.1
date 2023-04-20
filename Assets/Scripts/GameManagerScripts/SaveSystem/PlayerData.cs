using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    InventoryScript inv;
    SpriteRenderer spriteRenderer;
    GameObject sceneLoaderManager;
    Transform player;
    damageManager playerStats;
    LevelSystem levelSystem;
    Vector2 spawnPos;
    GameStart gameStart;
    public PlayerSaveData playerSave = new PlayerSaveData();
    //private InventorySaveData inventorySave = new InventorySaveData();
    
    private void Awake()
    {
        gameStart = GameObject.Find("GameStarter").GetComponent<GameStart>();
        inv = GameObject.Find("Player").GetComponent<InventoryScript>();
        sceneLoaderManager = GameObject.Find("GameManager");
        playerStats = GameObject.Find("Player").GetComponent<damageManager>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        levelSystem = GameObject.Find("Player").GetComponent<LevelSystem>();
        spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            //if the player dies, they are sent back to their last save point
            if (!playerStats.IsAlive)
            {
                gameStart.LoadingScene();
            }
        }

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SaveManager.CurrentSaveData.playerSaveData.CurrentScene)
        {
            player.transform.position = SaveManager.CurrentSaveData.playerSaveData.playerPos;
        }
        else
        {
            player.transform.position = new Vector2(-6.14f, -2.2f);
        }
    }
    //Saves the player's data
    public void SavePlayerData()
    {
        //For now just saving the sword item if the player has it so they can attack
        for (int i = 0; i < inv.PlayerItems.Count; i++)
        {
            if (inv.PlayerItems[i].title == "Sword")
            {
                playerSave.swordName = inv.PlayerItems[i].title;
            }
        }
        playerSave.numOfSparks = inv.Sparks;
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
        playerSave = SaveManager.CurrentSaveData.playerSaveData;
        inv.Sparks = playerSave.numOfSparks;
        playerStats.CurrentHealth = playerSave.CurrentHealth;
        playerStats.MaxHP = playerSave.MaxHP;
        levelSystem.CurrentXP = playerSave.XP;
        levelSystem.PlayerLevel = playerSave.level;
        inv.Sparks = playerSave.numOfSparks;
        if (playerSave.swordName != null)
        {
            //add code here
        }
        playerStats.IsAlive = true;    
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
    public int numOfSparks;
    public string swordName;
    public string test;
    //add a public int killCount?
}