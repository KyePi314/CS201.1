using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIinventory : MonoBehaviour
{
    TMP_Text itemCounter;
    public List<UIitem> uIitems = new List<UIitem>();
    public List<string> counters = new List<string>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    private int itemAmount;
    InventoryScript inv;
    public List<int> slotNum;
    private int slotNumber;
    int numSlots = 10;


    public int SlotNumber
    {
        get { return slotNumber; }
        set { slotNumber = value; }
    }

    private void Awake()
    {
        SlotNumber = 0;
        inv = GameObject.Find("Player").GetComponent<InventoryScript>();
        //Setting up to correct amount of inventory slots
        for (int i = 0; i < numSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uIitems.Add(instance.GetComponentInChildren<UIitem>());
        }
        itemCounter = GameObject.Find("Item").GetComponentInChildren<TMP_Text>();
        
    }
    private void Start()
    {
        itemAmount = 1;
    }
    public void UpdateSlot(int slot, Item item)
    {
        //Shows or hides items
        uIitems[slot].UpdateItem(item);
        //Now updates the counter object to match the name of the object its actually counting!
        if (item !=  null)
        {
            uIitems[slot].GetComponentInChildren<TMP_Text>().name = item.title;
            //Sets the inital amount
            item.itemAmount = itemAmount;
            uIitems[slot].GetComponentInChildren<TMP_Text>().text = item.itemAmount.ToString();
            SlotNumber++;
            item.slotNum = SlotNumber;
        }
        else
        {
            itemAmount = 0;
            uIitems[slot].GetComponentInChildren<TMP_Text>().text = itemAmount.ToString();
            uIitems[slot].GetComponentInChildren<TMP_Text>().name = "";
        }
        
    }
    private void Update()
    {
        
    }
    //Adds new items to the inventory UI 
    public void AddNewItem(Item item)
    {
        //Checking for empty slots
        var index = uIitems.FindIndex(i => i.item == null);
        //Runs if there is an empty slot
        if (index > -1)
        {
            //Calls the method to check if the item already exists in the inventory
            Item itemToAdd = inv.CheckForItems(item.id);
            //If it does, the item is stacked
            if (itemToAdd != null)
            {
                AddExistingItem(item);
            }
            //If not, the item is added to an empty slot
            else
            {
                UpdateSlot(index, item);
            }
            
        }
        //If theres no empty slots or existing items that match the item being picked up, the player cannot pick up the new item
        else
        {
            Debug.LogWarning("Inventory Full!");
        }
    }
    public void AddExistingItem(Item item)
    {
        //Loops through the items in the uiItems list
        for (int i = 0; i < uIitems.Count; i++)
        {
            //if the item found matches the item that was passed into the function
            if (uIitems[i].item == item)
            {
                //Incremets the amount of that particular item that the player has
                item.itemAmount += itemAmount;
                //Updates the text to this new amount
                var txt = uIitems[i].GetComponentInChildren<TMP_Text>();
                txt.text = item.itemAmount.ToString();
                break;
            }
        }
    }
    public void RemoveItem(Item item)
    {
        Debug.Log("Removed correct item" + item.title);

        //removes items
        UpdateSlot(uIitems.FindIndex(i => i.item == item), null);
    }
}
