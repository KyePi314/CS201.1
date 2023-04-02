using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
