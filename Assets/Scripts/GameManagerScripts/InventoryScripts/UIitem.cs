using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIitem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private Image spriteImage;
    private ToolTip toolTip;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        toolTip = GameObject.Find("ToolTip").GetComponent<ToolTip>();
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //If the pointer is hovering over an item in the inventory bar, it'll generate the tool tip
        if (this.item != null)
        {
            toolTip.GenerateTip(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }
}
