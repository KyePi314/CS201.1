using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InventoryScript : MonoBehaviour
{
    //Super basic inventory system for the prototype, also handles collecting sparks 
    
    private int sparks;
    SparkCounter counter;
    public List<Item> PlayerItems = new List<Item>();
    public ItemDatabase itemDatabase;
    private static InventoryScript inventoryInstance;
    public int Sparks
    {
        get
        {
            return sparks;
        }
        set
        {
            sparks = value;
        }
    }
    public static InventoryScript InventoryInstance
    {
        get
        {
            if (inventoryInstance == null)
            {
                Debug.LogError("Player has no Inventory");
            }
            return inventoryInstance;
        }
    }
    private void Awake()
    {
        
        inventoryInstance = this;
    }
    private void Start()
    {
         
    }
    // Update is called once per frame
    void Update()
    {
        //Waits until the game loads to start handling spark collection or finding the text component to update.
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            counter = GameObject.Find("SparkCounter").GetComponent<SparkCounter>();
            counter.OnSparkCollection(sparks);

        }
        
    }
    //Gets the items based off the ID
    public void GiveItems(int id)
    {
        Item itemToGive = itemDatabase.GetItem(id);
        PlayerItems.Add(itemToGive);
        Debug.Log("Added item: " + itemToGive.id);
    }

    //Gives the items based off the name
    public void GiveItems(string name)
    {
        Item itemToGive = itemDatabase.GetItem(name);
        PlayerItems.Add(itemToGive);
        Debug.Log("Added item " + itemToGive.name);
    }
    //Checks to see if an item is in the inventory
    public Item CheckForItems(int id)
    {
        return PlayerItems.Find(item =>  item.id == id);
    }
    //Removes items from the inventory
    public void RemoveItem(int id)
    {
        Item item = CheckForItems(id);
        if (item != null)
        {
            PlayerItems.Remove(item);
            Debug.Log("Item Removed" + item.name);
        }
    }
    public void CollectSparks()
    {
        sparks++;
        
    }
}
