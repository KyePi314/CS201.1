using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SparkCounter : MonoBehaviour
{
    TMP_Text counterText;
    public InventoryScript player;

    private void Awake()
    {
        counterText = GameObject.Find("SparkCounter").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").GetComponent<InventoryScript>();
        if (player == null)
        {
            Debug.LogWarning("No Player in scene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        counterText.text = player.Sparks + " / 10";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSparkCollection(int sparks)
    {
        counterText.text = sparks + " / 10";
    }
}
