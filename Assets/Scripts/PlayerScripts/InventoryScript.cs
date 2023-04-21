using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    //Super basic inventory system for the prototype, also handles collecting sparks 
    
    private int sparks;
    SparkCounter counter;
    public UIitem uiItems;
    public List<Item> PlayerItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIinventory inventory;
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
        counter = GameObject.Find("SparkCounter").GetComponent<SparkCounter>();
        
        itemDatabase = GameObject.FindWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        inventory = GameObject.Find("Inventory").GetComponent<UIinventory>();
    }
    private void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    //Gets the items based off the ID
    public void GiveItems(int id)
    {
        Item itemToGive = itemDatabase.GetItem(id);
        inventory.AddNewItem(itemToGive);
        PlayerItems.Add(itemToGive);
    }

    //Gives the items based off the name
    public void GiveItems(string name)
    {
        Item itemToGive = itemDatabase.GetItem(name);
        inventory.AddNewItem(itemToGive);
        PlayerItems.Add(itemToGive);
    }
    //Checks to see if an item is in the inventory
    public Item CheckForItems(int id)
    {
        return PlayerItems.Find(item => item.id == id);
    }
    //Removes items from the inventory
    public void RemoveItem(int id)
    {
        Item item = CheckForItems(id);
        if (item != null)
        {
            inventory.RemoveItem(item);
            PlayerItems.Remove(item);
            Debug.Log("Item Removed" + item.title);
        }
    }
    
    public void CollectSparks()
    {
        sparks++;
        counter.OnSparkCollection(sparks);
    }
}
/*TO-DO:
 * Figure out how to save the inventory as part of the save/load feature
 * 
*/


//Code for saving/loading inventory?:

//[System.Serializable]
//public struct InventorySaveData
//{
//    public La inventorySave;
//    public string test;
//    public string getJson()
//    {
//        return JsonUtility.ToJson(inventorySave);
//    }
//}

//public void saveInv()
//{
//    saveData.inventorySave = PlayerItems.Count;

//    Debug.Log("Test" +  " " + saveData.inventorySave + "");
//}