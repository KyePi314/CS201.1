using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public PlayerData player;
    [Header("Menu Buttons")]
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    PlayerData playerData;
    private void Awake()
    {
        player = GameObject.Find("SaveDataManager").GetComponent<PlayerData>();
    }
    //Handles when the new game button is clicked
    public void OnNewGameClicked()
    {
        //Sets the current scene to the menu so the game knows to set the player's position to its default starting point
        SaveManager.CurrentSaveData.playerSaveData.CurrentScene = 0;
        SaveManager.NewGame();
        player.SceneLoad(1);
        DisableAllButtons();
        
    }
    //Handles when the load game button is clicked
    public void onLoadGameClicked()
    {
        DisableAllButtons();
        player.LoadPlayerData();
    }

    private void DisableAllButtons()
    {
        newGame.interactable = false;
        loadGame.interactable = false;
    }
}
