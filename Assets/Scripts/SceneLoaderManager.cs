using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    
    public string entryName;
    public string previousScene;
    public PlayerController player;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("SceneExits"))
            {
                entryName = gameObject.name;
                previousScene = SceneManager.GetActiveScene().name;
                Debug.Log(previousScene);
                SceneManager.LoadSceneAsync(entryName);
                int s = SceneManager.GetActiveScene().buildIndex;


            }
        }
    }



}
