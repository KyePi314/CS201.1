using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkItem : MonoBehaviour
{
    KeepObjectsDestroyed collected;
    private void Awake()
    {
        collected = GameObject.Find("DestroyedObjManager").GetComponent<KeepObjectsDestroyed>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource clip = gameObject.GetComponent<AudioSource>();
            var pickup = clip.clip;
            AudioSource.PlayClipAtPoint(pickup, transform.position);
            InventoryScript.InventoryInstance.CollectSparks();
            collected.objects.Add(this.gameObject.name);
            Destroy(this.gameObject);    
        }
    }
}
