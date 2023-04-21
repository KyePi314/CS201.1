using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HotBar : MonoBehaviour
{
    GameObject heldItem;
    UIitem uiItem;
    PlayerController controller;
    UIinventory barSlot;
    InventoryScript player;

    public string itemName;
    private void Awake()
    {
        //Held item is a text item that displays the item that the player is currently holding
        heldItem = GameObject.Find("HeldItem");
        
        player = GameObject.Find("Player").GetComponent<InventoryScript>();
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
        barSlot = GameObject.Find("Inventory").GetComponent<UIinventory>();
    }
    private void Start()
    {
        heldItem.SetActive(false);    
    }
    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown((KeyCode)48 + i))
            {
                SelectedItem(i);
            }
        }
    }
    //Here the selected item(the item the player is holding) is gotten depending on the hotkey button pressed (1-9 and then the '-' for 10)
    public void SelectedItem(int input)
    {
        for (int i = 0; i < player.PlayerItems.Count; i++)
        {
            if (input == player.PlayerItems[i].slotNum)
            {
                controller.CurrentItemName = player.PlayerItems[i].id;
                heldItem.SetActive(true);
                heldItem.GetComponentInChildren<TMP_Text>().text = "Current Item: " + player.PlayerItems[i].title;
            }
        }
    }
}