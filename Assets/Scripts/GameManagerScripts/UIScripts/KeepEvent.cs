using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ensures the event system persists between scenes
public class KeepEvent : MonoBehaviour
{
    public static KeepEvent Instance { get; private set; }
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
