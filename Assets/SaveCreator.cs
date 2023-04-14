using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveCreator : MonoBehaviour
{
    GameObject player;
    public bool saveTrigger = false;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            saveTrigger = true;
        }
        
    }
}
