using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


//Very basic script to show how a quest system would work. The real quest system would be made up of multiple scripts, scriptable objects and classes
public class QuestsManager : MonoBehaviour
{
    damageManager damage;
    GameObject questManager;
    InventoryScript sparks;
    public TMP_Text questText;
    public bool isActive = true;
    private bool timeKeeper = false;
    private float textTimer = 3f;
    private float elapsedTimer = 0;
    [SerializeField]
    private int enemiesKilled;
    QuestStatus status;
    [SerializeField]
    private bool questActive;
    public bool questCompleted = false;
    string newQuest;
    [SerializeField]
    string finishQuest;
    public List<string> activeQuests;
    public List<string> CompletedQuests;
    public bool IsQuestActive
    {
        get { return questActive; }
        set
        {
            questActive = value;
        }
    }public bool IsQuestCompleted
    {
        get { return questCompleted; }
        set
        {
            questCompleted = value;
        }
    }
    public string FinishQuest
    {
        get { return  finishQuest; }
        set { finishQuest = value;}
    }
    public int EnemiesKilled
    {
        get { return enemiesKilled; }
        set { enemiesKilled = value; }
    }
    public enum QuestStatus
    {
        Pending,
        Active,
        Completed
    }
    private void Awake()
    {
        status = QuestStatus.Pending;
        questManager = GameObject.Find("QuestManager");
        questText = questManager.GetComponentInChildren<TMP_Text>();
        damage = GameObject.Find("Player").GetComponent<damageManager>();
        sparks = GameObject.Find("Player").GetComponent<InventoryScript>();
        questText.text = null;
    }
    private void Update()
    {
        //Timer for how long the text for the quest is on the screen for
        if (timeKeeper)
        {
            elapsedTimer += Time.deltaTime;
            if (elapsedTimer > textTimer)
            {
                questText.text = null;
                timeKeeper = false;
                
                questCompleted = false;
                elapsedTimer = 0;
                Debug.Log(elapsedTimer + " " + timeKeeper);
            }
        }
        if (isActive)
        {
            questManager.SetActive(true);
        }
        else
        {
            questManager.SetActive(false);
        }
        //Checks if a quest has been completed or not, and passes the name of the completed quest to the quest manager function
        if (questCompleted)
        {
            QuestTypeManager(FinishQuest);
        }
    }
    void AddObjectToList(string obj)
    {
        if (!CompletedQuests.Contains(obj))
            CompletedQuests.Add(obj);

    }
    //Handles which quests to load informtation for depending on the name passed to the function
    public void QuestTypeManager(string name)
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i] == name)
            {
                newQuest = name;
                break;
            }
        }
        
        switch (newQuest)
        {
            case "Gort":
                damage.EnemyName = "Golem";
                GolemQuest();
                break;
            case "NPC_2":
                status = QuestStatus.Active;
                MasterQuestline();
                break;
            default: 
                break;
        }
    }
    //Gorts quest to kill golems
    public void GolemQuest()
    {
        if (EnemiesKilled < 5)
        {
            timeKeeper = true;
            IsQuestActive = true;
            activeQuests.Add("Gort");
            questText.text = "Gort's Golem Issue: Quest Started";
           
        }
        else if (EnemiesKilled >= 5 && questCompleted)
        {
            finishQuest = "Gort";
            AddObjectToList(FinishQuest);
            timeKeeper = true;
            IsQuestActive = false;
            questText.text = "Gorts's Golem Issue: Quest Completed!";
        }

    }
    //Main overarching quest, in a future update this would spawn a mini boss in at the end
    public void MasterQuestline()
    {
        
        if (status == QuestStatus.Active && sparks.Sparks < 8)
        {
            timeKeeper = true;
            IsQuestActive = true;
            activeQuests.Add("NPC_2");
            questText.text = "Saviour of the Forest: Started";
        }
        else if (sparks.Sparks == 8)
        {
            timeKeeper = true;
            IsQuestActive = false;
            questText.text = "Saviour of the Forest: Completed";
            finishQuest = "NPC_2";
        }
    }
}
