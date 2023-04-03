using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script has the virtual camera find the player each time the scene changes, ensuring the camera continues to follow the player despite changing scenes
public class FindPlayerAtStart : MonoBehaviour
{
    //Makes sure the camera doesn't get destroyed when a new scene loads
    public static FindPlayerAtStart Instance;
    void Start()
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

    private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        vCam.enabled = false;
        vCam.enabled = true; //only way I found to recenter the virtual camera on the player each time a new scene loads up
    }
}
