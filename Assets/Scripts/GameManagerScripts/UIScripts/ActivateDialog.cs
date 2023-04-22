using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialog : MonoBehaviour
{
    public TextAsset textAssest;
    private string s;
    public int startLine;
    public int endLine;
    public bool pressKey;
    private bool PlayerReward = false;
    private bool waitForPress;
    public TextBoxManager textBox;
    public bool destroyAfterSpeech;
    KeepObjectsDestroyed destroyed;
    LevelSystem levels;
    QuestsManager questsManager;
    InventoryScript playerInv;
    // Start is called before the first frame update
    void Start()
    {
        destroyed = GameObject.Find("DestroyedObjManager").GetComponent<KeepObjectsDestroyed>();
        levels = GameObject.Find("Player").GetComponent<LevelSystem>();
        questsManager = GameObject.Find("QuestManager").GetComponent<QuestsManager>();
        playerInv = GameObject.Find("Player").GetComponent<InventoryScript>();
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
            if (playerInv.CheckForItems(0) == null)
            {
                playerInv.GiveItems(0);
            }
            textBox.EnableSpeech();

             for (int i = 0; i < questsManager.activeQuests.Count; i++)
            {

                ActivateDialog[] obj = GameObject.FindObjectsOfType<ActivateDialog>();
                for (int j = 0; j < obj.Length; j++)
                {
                    if (obj[j].name == questsManager.activeQuests[i])
                    {
                        PlayerReward = true;
                        questsManager.FinishQuest = obj[j].name;
                        questsManager.questCompleted = true;
                        break;
                    }
                }

                if (PlayerReward)
                {
                    GivePlayerReward();
                }
                
            }
            
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
            if (textAssest != null)
            {
                textBox.ReloadSpeechScript(textAssest);
                textBox.textLine = startLine;
                textBox.lastLine = endLine;
                textBox.EnableSpeech();
            }
            
            if (gameObject.tag.Equals("Quest"))
            {
                questsManager.activeQuests.Add(gameObject.name);
                questsManager.QuestTypeManager(gameObject.name);
            }
            if (destroyAfterSpeech)
            {
                destroyed.objects.Add(gameObject.name);
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
    public void GivePlayerReward()
    {
       
        levels.updateXP(20);
        playerInv.GiveItems("Heart");
    }
}
