using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject TextBox;
    Animator animator;
    public TMP_Text speechText;
    public TextAsset textFile;
    public string[] speech;

    public int textLine;
    public int lastLine;
    public bool isActive = false;
    public bool canMove;

    public PlayerController player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = player.GetComponent<Animator>();
        TextBox = GameObject.Find("DialogBox");
        speechText = TextBox.GetComponentInChildren<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (textFile != null)
        {
            //Adding text from the text file, with the new line splitting the text lines into seperate parts to be added in the array.
            speech = (textFile.text.Split('\n'));
        }
        //sets the final line number
        if (lastLine == 0)
        {
            lastLine = speech.Length - 1;
        }

        if (isActive)
        {
            EnableSpeech();
        }
        else
        {
            DisableSpeech();
        }
    }
    private void Update()
    {
        
        if (!isActive )
        {
            return;
        }
        //Reads player input so the player can go through the dialog
        if (isActive && Input.GetKeyDown(KeyCode.Return))
        {
            textLine += 1;
        }
        if (textLine < lastLine)
        {
            speechText.text = speech[textLine];
        }
        //Closes the text box after the last line of dialog
        else if (textLine > lastLine)
        {
            DisableSpeech();
        }
    }

    public void EnableSpeech()
    {
        TextBox.SetActive(true);
        isActive = true;
    }

    public void DisableSpeech()
    {
        TextBox.SetActive(false );
        isActive = false;
    }

    public void ReloadSpeechScript(TextAsset texts)
    {
        if (texts != null)
        {
            speech = new string[1];
            speech = (texts.text.Split('\n'));
        }
    }
}
