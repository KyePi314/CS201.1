using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryScript Inventory;
    UIinventory inv;
    private void Awake()
    {
        inv = GameObject.Find("Inventory").GetComponent<UIinventory>();

    }
    //Handles if the player has come across a collectable
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            Inventory.GiveItems(other.gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
