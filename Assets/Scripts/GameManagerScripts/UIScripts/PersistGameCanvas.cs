using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistGameCanvas : MonoBehaviour
{
    public static PersistGameCanvas Instance { get; set; }

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
    }
}
