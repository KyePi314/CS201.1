using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*TO-DO: After Prototype
 * Rework the main menu and game management systems now I have a better understanding of them so that I dont have to have the player or the UI for the game screen in the main menu
 * */
public class Menu : MonoBehaviour
{
    
    GameStart game;
    [Header("Menu Buttons")]
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    private string buttonPress;

    public string ButtonPressed
    {
        get { return buttonPress; }
        set { buttonPress = value; }
    }
    PlayerData playerData;
    private void Awake()
    {
       
    }
    //Handles when the new game button is clicked
    public void OnNewGameClicked()
    {
        SaveManager.NewGame();
        SceneManager.LoadSceneAsync(1);
        
    }
    //Handles when the load game button is clicked
    public void onLoadGameClicked()
    {
        SaveManager.LoadGame();
        SceneManager.LoadSceneAsync(1);
    }

    private void DisableAllButtons()
    {
        newGame.interactable = false;
        loadGame.interactable = false;
    }
}
