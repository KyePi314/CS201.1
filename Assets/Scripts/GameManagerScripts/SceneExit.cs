using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script is set to the various exits that are in the scenes
 * it has the code for the trigger and saves the data that needs to be passed to the script attached to the global game manager that doesn't get destroyed on load
 * */
public class SceneExit : MonoBehaviour
{
    public SceneLoaderManager SceneLoaderManager;
    //Variables
    public string exitName;
    public string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneLoaderManager = GetComponent<SceneLoaderManager>();
        if (gameObject.CompareTag("SceneExits") && other.tag.Equals("Player")) //When the player enters the trigger, it will check the they entered the scene exit tagged zone
        {
            sceneToLoad = gameObject.name; //Save the exit name to the variable which is the same as the name of the scene to load
            exitName = SceneManager.GetActiveScene().name; //Gets the name of the current scene, which is saved and will corralate to the name of the spawn pos in the next scene
           
            PlayerPrefs.SetString("Last Exit Name", exitName); //stores the exit name for future use.
            PlayerPrefs.SetString("Scene Name", sceneToLoad); //Stores the name to the next scene to load
            SceneLoaderManager.Instance.SceneSwap(); //calls the class from the global scene loader manager script
        }
        
    }

}
