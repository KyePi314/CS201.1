using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List <Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }
    //Finds the item using its ID
    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }
    public Item GetItem(string name)
    {
        return items.Find(item => item.name == name);
    }
    //Building a basic database of items, not all of these will be in the prototype though
    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "Sword", "A mighty sword to slay enemies with, don't mind the rusting",
            new Dictionary<string, int>
            {
                {"Power", 10 },
                {"Speed", 5 }
            }),

            new Item(1, "Axe", "More powerful than the sword, but heavier and slower to move",
            new Dictionary<string, int>
            {
                {"Power", 15 },
                {"Speed", 3 }
            }),
            new Item(2, "Coin", "You can probably use these to buy cool stuff if you get enough of them", 
            new Dictionary<string, int>
            {
                {"Value", 1 }
            }),
            new Item(3, "Heart", "Not a real heart. But maybe it will give you some health?",
            new Dictionary<string, int>
            {
                {"Regen", 50 }
            }),
            new Item(4, "Meat", "A tasty snack, don't ask what kind of meat it is, its a mystery!", 
            new Dictionary<string, int>
            {
                {"Regen", 10 }
            }),
            new Item(5, "SpeedPotion", "Drink Me! I'll give you zoomies", new Dictionary<string, int>
            {
                {"Speed", 5 }
            }),
            new Item(6, "Spark", "A weird, glowing thing. Seems kinda important try collecting them all and see if anything happens", new Dictionary<string, int>
            {
                {"Amount", 1 }
            })
        };
    }
}