using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveCreator : MonoBehaviour
{
    damageManager damageManager;
    PlayerData playerData;
    GameObject player;
    public bool saveTrigger = false;
    private void Awake()
    {
        player = GameObject.Find("Player");
        playerData = GameObject.Find("SaveDataManager").GetComponent<PlayerData>();
        damageManager = player.GetComponent<damageManager>();
    }
    private void Update()
    {
        if (saveTrigger)
        {
            //Saves the game at the save point
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioSource saved = GameObject.Find("SaveSound").GetComponent<AudioSource>();
                var savedPlay = saved.clip;
                saved.PlayOneShot(savedPlay);
                playerData.SavePlayerData();
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            saveTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            saveTrigger = false;
        }
    }
}