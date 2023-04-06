using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script just ensures that the player doesn't get destroyed when moving between scenes
public class PlayerSpawnControl : MonoBehaviour
{
 
    public static PlayerSpawnControl Instance;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    
}
