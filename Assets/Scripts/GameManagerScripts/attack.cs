using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public int attackPower = 10;
    public Vector2 knockback = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        takeDamage damageable = collision.GetComponent<takeDamage>();
        bool gotHit = damageable.Hit(attackPower, knockback);
        if (damageable != null)
        {
         if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackPower);
            }   
        }
    }
}
