using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public SceneLoaderManager SceneLoaderManager;

    public string exitName;
    public string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneLoaderManager = GetComponent<SceneLoaderManager>();
        if (gameObject.CompareTag("SceneExits")) //When the player enters the trigger, it will check the they entered the scene exit tagged zone
        {
            sceneToLoad = gameObject.name; //Save the exit name to the variable which is the same as the name of the scene to load
            exitName = SceneManager.GetActiveScene().name; //Gets the name of the current scene, which is saved and will corralate to the name of the spawn pos in the next scene
           
            PlayerPrefs.SetString("Last Exit Name", exitName); //stores the exit name for future use.
            PlayerPrefs.SetString("Scene Name", sceneToLoad); //Stores the name to the next scene to load
            SceneLoaderManager.Instance.SceneSwap(); //calls the class from the global scene loader manager script
        }
        
    }

}
