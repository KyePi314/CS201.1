using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialog : MonoBehaviour
{
    public TextAsset textAssest;

    public int startLine;
    public int endLine;
    public bool pressKey;
    private bool waitForPress;
    public TextBoxManager textBox;
    public bool destroyAfterSpeech;
    QuestsManager questsManager;
    InventoryScript checkForSword;
    // Start is called before the first frame update
    void Start()
    {
        questsManager = GameObject.Find("QuestManager").GetComponent<QuestsManager>();
        checkForSword = GameObject.Find("Player").GetComponent<InventoryScript>();
        textBox = FindObjectOfType<TextBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the dialog attached to the gameobject that requires player interaction to start
        if (waitForPress && Input.GetKeyDown(KeyCode.F))
        {
            textBox.ReloadSpeechScript(textAssest);
            textBox.textLine = startLine;
            textBox.lastLine = endLine;
            //If the player has no sword (the start of the game) the first npc will give them one.
            if (checkForSword.CheckForItems(0) == null)
            {
                checkForSword.GiveItems(0);
            }
            textBox.EnableSpeech();
             
            
            waitForPress = false;
            if (destroyAfterSpeech)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (pressKey)
            {
                waitForPress = true;
                return;
            }
            textBox.ReloadSpeechScript(textAssest);
            textBox.textLine = startLine;
            textBox.lastLine = endLine;
            textBox.EnableSpeech();
            if (gameObject.tag.Equals("Quest"))
            {
                questsManager.StartQuest(gameObject.name);
            }
            if (destroyAfterSpeech)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForPress = false;
            
        }
    }
}
