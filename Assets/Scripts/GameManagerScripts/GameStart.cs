using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    GameObject DialogBox;
    Transform player;
    PlayerData playerData;
    Menu menu;
    SceneLoaderManager sceneLoaderManager;
    public static GameStart instance { get; set; }
    private void Awake()
    {
        sceneLoaderManager = GameObject.Find("SceneLoader").GetComponent<SceneLoaderManager>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerData = GameObject.Find("SaveDataManager").GetComponent<PlayerData>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject); //Checks for any duplications and deletes them, allowing there to only be one instance of GameManager object in the scene.
        }
        //Handles ensuring that the object that uses this script doesn't get destroyed when a scene loads, allowing its data to move between scenes 
        DontDestroyOnLoad(gameObject);
        sceneLoaderManager.enabled = false;
        DialogBox = GameObject.Find("DialogBox");

    }   

    private void Start()
    {
        Debug.Log("Pls");
        DialogBox.SetActive(false);
        LoadingScene();
        
    }

    public void LoadingScene()
    {
        if (SaveManager.CurrentSaveData.playerSaveData.CurrentScene != 0)
        {
            var scene = SaveManager.CurrentSaveData.playerSaveData.CurrentScene;
            StartCoroutine(LoadSceneAsync(scene));
        }
        else
        {
            SaveManager.CurrentSaveData.playerSaveData.CurrentScene = 0;

            StartCoroutine(LoadSceneAsync(2));
        }
    }
    IEnumerator LoadSceneAsync(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
      
        if (asyncLoad.isDone)
        {
            if (SaveManager.CurrentSaveData.playerSaveData.CurrentScene != 0)
            {
                player.transform.position = SaveManager.CurrentSaveData.playerSaveData.playerPos;
                playerData.LoadPlayerData();
            }
            sceneLoaderManager.enabled = true;
        }
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}
    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
    //public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
        
    //}

    //public void OnSceneUnloaded(Scene scene, LoadSceneMode mode)
    //{

    //}
}