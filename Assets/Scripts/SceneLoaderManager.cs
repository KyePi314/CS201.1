using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public string spawn;
    public Vector2 spawnPointTest;
    public string prevLevel;

    public int sceneExit;
    public string entryName;
    public string previousScene { get; private set; }
    public PlayerController player;
    public GameControl gameControl;
   

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) //Checks that it was the player entering the trigger
        {
            if (gameObject.CompareTag("SceneExits")) //Checks the trigger was a SceneExit one
            {
                entryName = gameObject.name; //Gets the name of the trigger which will correspond with which scene will spawn
                previousScene = gameObject.scene.name;
                StartCoroutine(LoadAsyncScene());
            }
                
        }
    }


    IEnumerator LoadAsyncScene()
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(entryName);
        while (!loadingScene.isDone)
        {
            yield return SceneManager.LoadSceneAsync(entryName);
        }
       
    }

    

}
