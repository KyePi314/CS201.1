using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Item
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public int slotNum;
    public int itemAmount = 1;

    //Storing stats for different items
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Item(int id, int itemAmount, string title, string description,Dictionary<string, int> stats)
    {
        this.id = id;
        this.itemAmount = itemAmount;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.stats = stats;
      
    }
    
    public Item(Item item)
    {
        this.id = item.id;
        this.itemAmount = item.itemAmount;
        this.title = item.title;
        this.description= item.description; 
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
        this.stats = item.stats;
    }
}