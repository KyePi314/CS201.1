using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Handles the tooltip that is displayed when hovering over the items in the inventory
public class ToolTip : MonoBehaviour
{
    private TMP_Text toolTip;
    GameObject tpText;
    private void Awake()
    {
        toolTip = GetComponentInChildren<TMP_Text>();
        tpText = GameObject.Find("tipText");
        
    }
    private void Start()
    {
        gameObject.SetActive(false);
        tpText.SetActive(false);
    }
    private void Update()
    {

    }
    public void GenerateTip(Item item)
    {
        string statsTxt = "";
        if (item.stats.Count > 0)
        {
            foreach (var stat in item.stats)
            {
                statsTxt += stat.Key.ToString() + ": " + stat.Value.ToString() + "\n" ;
            }
        }
        string tooltip = string.Format("<b>{0}\n{1}\n\n<b>{2}</b>", item.title, item.description, statsTxt);
        toolTip.text = tooltip;
        tpText.SetActive(true);
        gameObject.SetActive(true);
    }
}