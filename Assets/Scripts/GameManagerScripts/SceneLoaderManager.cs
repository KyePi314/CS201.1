using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
   //Variables needed for this class
    public Vector2 spawnPoint; //Stores the spawn point needed
    public string sceneToLoad; 

    //Game objects and objects of other classes
    public SceneExit sceneLoad;
    public GameObject[] spawnPoints; //An array for all spawn points in the new scene
    public GameObject player; 

    public static SceneLoaderManager Instance {  get; private set; } //Used to ensure that the gameManager object that is attached to this script isnt destroyed or duplicated

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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            spawnPoint = new Vector2(-6.14f, -2.2f);
        }
        else
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawn"); //Finds and stores all the spawn entrance points in the level
            for (int i = 0; i < spawnPoints.Length; i++) //Loops through the spawn points so we can find the correct one that we need
            {
                if (spawnPoints[i].name == PlayerPrefs.GetString("Last Exit Name"))
                {
                    spawnPoint = spawnPoints[i].transform.position;
                    //player = GameObject.FindWithTag("Player");   
                    //player.transform.position = spawnPointTest;
                    break;
                }
            }
        }
        player = GameObject.FindWithTag("Player");
        spawnPlayer(spawnPoint);
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
           spawnPoint = Vector2.zero;
           if (PlayerPrefs.HasKey("Last Exit Name"))
           {
               spawnPoints = GameObject.FindGameObjectsWithTag("Spawn"); //Finds and stores all the spawn entrance points in the level
               for (int i = 0; i < spawnPoints.Length; i++) //Loops through the spawn points so we can find the correct one that we need
               {
                   if (spawnPoints[i].name == PlayerPrefs.GetString("Last Exit Name"))
                   {
                       spawnPoint = spawnPoints[i].transform.position;
                       spawnPlayer(spawnPoint);
                       //player = GameObject.FindWithTag("Player");   
                       //player.transform.position = spawnPointTest;
                       break;
                   }
               }
           }
           else
           {
               spawnPoint = Vector2.zero;
           }
    }

    void spawnPlayer(Vector2 spawnPoint)
    {
        player.transform.position = spawnPoint;
    }


}
