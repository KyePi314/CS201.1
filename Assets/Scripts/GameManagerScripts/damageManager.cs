using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class damageManager : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> changedHealth;
    //Object references etc
    ItemDatabase inv;
    Animator animator;
    LevelSystem levelSystem;
    EnemyLevels enemyLevels;
    SpriteRenderer spriteRenderer;
    GameObject objRemoved;
    public GameObject Coin;
    public GameObject Meat;
    public GameObject SpeedPotion;
    AudioSource hitSound;
    GameObject Prefab;
    Color startColor;
    //Variables
    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _health;
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float lastHitTimer = 0;
    private float invincibiltyTimer = 0.25f;
    private int _hitPower;
    public float timer = 0.5f;
    private float timeElapsed = 0;
    public List<Transform> ItemDrop = new List<Transform>();
    public int MaxHP
    {
        get
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;
        }
    }
    public int CurrentHealth
    {
        get
        {
            return _health;

        }
        set
        {
            _health = value;
            changedHealth?.Invoke(_health, MaxHP);
        }
    }
    public int attackHit
    {
        get
        {
            return _hitPower;
        }
        set
        {

            _hitPower = value;
        }
    }

    public bool IsAlive
    {
        get
        {

            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimStrings.isAlive, value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimStrings.lockVelocity, value);
        }
    }
    private void Awake()
    {
        inv = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        animator = GetComponent<Animator>();
        levelSystem = GameObject.Find("Player").GetComponent<LevelSystem>();
        enemyLevels = GetComponent<EnemyLevels>();
        hitSound = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (isInvincible)
        {
            if (lastHitTimer > invincibiltyTimer)
            {
                isInvincible = false;
                lastHitTimer = 0;
            }

            lastHitTimer += Time.deltaTime;
        }
        if (!IsAlive && gameObject.tag.Equals("Enemy") || gameObject.tag.Equals("flyingEnemy"))
        {
            timeElapsed = 0f;
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            startColor = spriteRenderer.color;
            objRemoved = animator.gameObject;

            timeElapsed += Time.deltaTime;
            float newAlpha = startColor.a * (1 - (timeElapsed / timer));
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if (timeElapsed > timer)
            {
                Destroy(objRemoved);
            }
        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            //Plays sound on the object being hit
            var beenHit = hitSound.clip;
            hitSound.PlayOneShot(beenHit);
            CurrentHealth -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimStrings.hitTrigger);
            LockVelocity = true;
            if (CurrentHealth <= 0)
            {
                //Plays sound on death
                AudioSource deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
                var playSound = deathSound.clip;
                deathSound.PlayOneShot(playSound);
                IsAlive = false;
                if (gameObject.tag.Equals("Enemy") || gameObject.tag.Equals("flyingEnemy"))
                {
                    levelSystem.updateXP(enemyLevels.enemyXP);
                    RandomLoot();
                }

            }

            damageableHit?.Invoke(damage, knockback);
            return true;

        }
        else
        {
            return false;
        }

    }

    public void RandomLoot()
    {
        
        //Gets a random number between 1 and 3
        int rndNum = Random.Range(1, 3);
        //Checks which number has been saved from the number randomizer and uses that to spawn in the prefab item that will be dropped as loot from the enemy
        if (rndNum == 1)
        {
           
            Prefab = Instantiate(Coin, transform.position, Quaternion.identity);
            Prefab.name = "Coin";
        }
        else if (rndNum == 2)
        {
            Prefab = Instantiate(Meat, transform.position, Quaternion.identity);
            Prefab.name = "Meat";
        }
        else if (rndNum == 3)
        {
            Prefab = Instantiate(SpeedPotion, transform.position, Quaternion.identity);
            Prefab.name = "SpeedPotion";
        }
        Prefab.tag = "Collectable";
        
        Debug.Log(rndNum.ToString());
    }
    public void lootDrop(string loot)
    {
        Debug.Log(loot);
        
    }
}