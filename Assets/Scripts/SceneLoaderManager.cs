using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
   
    public Vector2 spawnPointTest;
    public string sceneToLoad;
    public string exitName;


    public SceneExit sceneLoad;
    public GameObject[] spawnPoints;
    public GameObject player;

    public static SceneLoaderManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject); //Checks for any duplications and deletes them, allowing there to only be one instance of GameManager object in the scene.
        }
        //Handles ensuring that the object that uses this script doesn't get destroyed when a scene loads, allowing its data to move between scenes 
        DontDestroyOnLoad(gameObject);
        //Need to find a better way of executing the below code but it works for now, even if its not as simple as it should be and does double up
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            spawnPointTest = Vector2.zero;
        }
        else
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawn"); //Finds and stores all the spawn entrance points in the level
            for (int i = 0; i < spawnPoints.Length; i++) //Loops through the spawn points so we can find the correct one that we need
            {
                if (spawnPoints[i].name == PlayerPrefs.GetString("Last Exit Name"))
                {
                    spawnPointTest = spawnPoints[i].transform.position;
                    //player = GameObject.FindWithTag("Player");   
                    //player.transform.position = spawnPointTest;
                    break;
                }
            }
        }
        player = GameObject.FindWithTag("Player");
        spawnPlayer(spawnPointTest);
    }

   

    public void SceneSwap()
    {
        StartCoroutine(LoadSceneAsync()); //Calls the method that starts loading the scene 
    }



    IEnumerator LoadSceneAsync()
    {
        sceneToLoad = PlayerPrefs.GetString("Scene Name"); //Retrieves the string that determines which scene to load based on which exit the player went through
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad); //Starts loading the scene
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        spawnPointTest = Vector2.zero;
            if (PlayerPrefs.HasKey("Last Exit Name"))
            {
                exitName = PlayerPrefs.GetString("Last Exit Name"); //The entrance I need the player to spawn at needs to corralate to the right spawn point for that entrance by using this variable
                spawnPoints = GameObject.FindGameObjectsWithTag("Spawn"); //Finds and stores all the spawn entrance points in the level
                for (int i = 0; i < spawnPoints.Length; i++) //Loops through the spawn points so we can find the correct one that we need
                {
                    if (spawnPoints[i].name == PlayerPrefs.GetString("Last Exit Name"))
                    {
                        spawnPointTest = spawnPoints[i].transform.position;
                        spawnPlayer(spawnPointTest);
                        //player = GameObject.FindWithTag("Player");   
                        //player.transform.position = spawnPointTest;
                        break;
                    }
                }
            }
            else
            {
                spawnPointTest = Vector2.zero;
                //player.transform.position = spawnPointTest;
            }
     
        

 
    }

    void spawnPlayer(Vector2 spawnPoint)
    {

        player.transform.position = spawnPoint;
    }
    //public void spawnPlayer(Vector2 spawnPos)
    //{
    //    player = GetComponent<PlayerController>();
    //    player.transform.position = spawnPos;
    //}

    //    public GameObject player;
    //    public GameObject[] sceneWarpArr;


    //    public string spawn;
    //    public Vector2 spawnPointTest;
    //    public string prevLevel;
    //    public string currentSpawn;
    //    public int sceneExit;
    //    public string entryName;
    //    public static SceneLoaderManager instance = null;

    //    void Start()
    //    {
    //        ////Makes sure that there are no duplicates of the player in the first scene.
    //        //if (!playerExists)
    //        //{
    //        //    playerExists = true;
    //        //    DontDestroyOnLoad(transform.gameObject);
    //        //}
    //        //else
    //        //{
    //        //    Destroy(gameObject);
    //        //}

    //    }

    //    private void Awake()
    //    {
    //        if (instance == null)
    //        {
    //            DontDestroyOnLoad(gameObject);
    //            instance = this;
    //        }
    //        else if (instance != null)
    //        {
    //            Destroy(gameObject);
    //        }

    //        if (player == null)
    //        {
    //            player = GameObject.FindGameObjectWithTag("Player");
    //        }
    //        if (sceneWarpArr.Length == 0)
    //        {
    //            sceneWarpArr = GameObject.FindGameObjectsWithTag("SceneExits");
    //        }
    //    }


    //    private void OnLevelWasLoaded()
    //    {
    //        player = GameObject.FindGameObjectWithTag("Player");
    //        sceneWarpArr = GameObject.FindGameObjectsWithTag("SceneExits");

    //        for (int i =0; i< sceneWarpArr.Length; i++)
    //        {
    //            if (sceneWarpArr[i].GetComponent<SceneWarp>().entryName == currentSpawn)
    //            {
    //                player.transform.position = sceneWarpArr[i].transform.position;
    //            }
    //        }
    //    }

    //    public void Loadscene(string sceneExit)
    //    {
    //        Scene currentScene =  SceneManager.GetActiveScene();
    //        int buildIndex = currentScene.buildIndex;
    //        currentSpawn = sceneExit;
    //        if (buildIndex == 0)
    //        {
    //            SceneManager.LoadScene("Woodlands-1");
    //        }
    //        else if (buildIndex == 1)
    //        {
    //            SceneManager.LoadScene("Clearing");
    //        }
    //    } //Find a way of checking the level and loading level due to if at left or right of scene.
    //    //IEnumerator LoadAsyncScene()
    //    //{
    //    //    AsyncOperation loadingScene = SceneManager.LoadSceneAsync(entryName);
    //    //    while (!loadingScene.isDone)
    //    //    {
    //    //        yield return SceneManager.LoadSceneAsync(entryName);
    //    //    }

    //    //}



}
