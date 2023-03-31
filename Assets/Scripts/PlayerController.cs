using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Variables
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Stores the Input.GetAxis result

        Vector2 position = transform.position; //Variable to store player position?
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        transform.position = position; //Tranforms the player's position based on the users input
       
    }
    private void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);

    }

}
