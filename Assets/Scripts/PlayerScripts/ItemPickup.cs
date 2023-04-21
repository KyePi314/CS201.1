using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryScript Inventory;
    Transform player;
    UIinventory inv;
    private void Awake()
    {
        Inventory = GameObject.Find("Player").GetComponent<InventoryScript>();
    }
    //Handles if the player has come across a collectable
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            AudioSource otherAudio = GameObject.Find("PickupSound").GetComponent<AudioSource>();   
            var pickupSound = otherAudio.clip;
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Inventory.GiveItems(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
