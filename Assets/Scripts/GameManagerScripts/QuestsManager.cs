using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private bool timeKeeper;
    public string QuestName;
    private float textTimer = 1f;
    private float elapsedTimer = 0;
    [SerializeField]
    private int enemiesKilled;
    QuestStatus status; 
    private bool questActive;
    public bool IsQuestActive
    {
        get { return questActive; }
        set
        {
            questActive = value;
        }
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
        if (timeKeeper)
        {
            timer();
        }
        if (isActive)
        {
            questManager.SetActive(true);
        }
        else
        {
            questManager.SetActive(false);
        }
        if (EnemiesKilled == 5)
        {
            GolemQuest();
        }
    }

    public void StartQuest(string name)
    {
        
        QuestName = name;
        switch (QuestName)
        {
            case "Gort":
                status = QuestStatus.Active;
                
                damage.EnemyName = "Golem";
                GolemQuest();
                break;
            case "NPC_2":
                break;
            default: 
                break;
        }
    }

    public void GolemQuest()
    {
        if (status == QuestStatus.Active && EnemiesKilled < 5)
        {
            timeKeeper = true;
            IsQuestActive = true;
           questText.text = "Gort's Golem Issue: Quest Started";
           
        }
        else if (EnemiesKilled == 5)
        {
            Debug.Log("Quest working");
           questText.text = "Gorts's Golem Issue: Quest Completed!";
            textTimer -= Time.deltaTime;
            timeKeeper = true;
            IsQuestActive = false;
            status = QuestStatus.Completed;
        }

    }
    private void timer()
    {
        elapsedTimer += Time.deltaTime;
        if (elapsedTimer > textTimer)
        {
            questText.text = null;
            timeKeeper = false;
        }
        
    }
    public void MasterQuestline()
    {
        status = QuestStatus.Pending;
        if (sparks.Sparks < 8)
        {
            status = QuestStatus.Active;
        }
        else if (EnemiesKilled == 8)
        {
            status = QuestStatus.Completed;
        }
    }
}
