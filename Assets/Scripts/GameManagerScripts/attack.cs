using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public int attackPower;

    public Vector2 knockback = Vector2.zero;
    
    //This handles the player and enemies hitting power and detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageManager damageable = collision.GetComponent<damageManager>();
        
        if (damageable != null)
        {
            //handles which direction the knock hits the object towards depending on the attacks faced direction
            Vector2 confirmedKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackPower, confirmedKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackPower);
            }   
        }
    }
}
