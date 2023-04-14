using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public TMP_Text healthText;
    public Slider healthSlider;
    public damageManager player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<damageManager>();
        if (player == null )
        {
            Debug.LogWarning("No Player in scene");
        }
    }
    private void Start()
    {
        healthSlider.value = calcSliderPercent(player.CurrentHealth, player.MaxHP);
        healthText.text = "HP " + player.CurrentHealth + " / " + player.MaxHP;
    }

    private float calcSliderPercent(float currentHP, float maxHP)
    {
        return currentHP / maxHP;
    }
    private void OnDisable()
    {
        player.changedHealth.RemoveListener(OnPlayerChangedHP);
    }
    private void OnEnable()
    {
        player.changedHealth.AddListener(OnPlayerChangedHP);
    }
    private void OnPlayerChangedHP(int updatedHealth, int maxHP)
    {
        healthSlider.value = calcSliderPercent(updatedHealth, maxHP);
        healthText.text = "HP " + updatedHealth + " / " + maxHP;
    }
}
