using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static bool playerExists;
    void Start()
    {  
        ////Makes sure that there are no duplicates of the player in the first scene.
        //if (!playerExists)
        //{
        //    playerExists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Stores the Input.GetAxis result
        Vector2 position = transform.position; //Variable to store player position?
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        transform.position = position; //Tranforms the player's position based on the users input
       
    }

}
