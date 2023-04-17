using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Animator animator;
    public DetectionZone stunZone;
    public DetectionZone attackZone;
    private bool _seesTarget = false;
    private bool isStunned = false;
    private float stunnedTime = 0.2f;

    public bool seesTarget
    {
        get
        {
            return _seesTarget;
        }
        set
        {
            _seesTarget = value;
            animator.SetBool(AnimStrings.seeTarget, value);
        }
    }
    //sets a cooldown for between attacks so the enemy can't keep attacking and give the player no chance to counter or escape
    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    public bool IsStunned
    {
        get
        {
            return isStunned;
        }
        set
        {
            isStunned = value;
            animator.SetBool(AnimStrings.isStunned, value);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        seesTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (stunZone.detectedColliders.Count > 0 && gameObject.tag.Equals("Enemy"))
        {
            enemyStunned();
        }

        if (isStunned)
        {
            StartCoroutine(StunnedTime());
        }
    }

    public void enemyStunned()
    {
        if (isStunned)
        {
            return;
        }
        IsStunned = true;
        Debug.Log("Enemy" + isStunned.ToString());
        StartCoroutine(StunnedTime());
    }
    //Handles how long the enemy is stunned for.
    IEnumerator StunnedTime()
    {
        yield return new WaitForSeconds(1.5f);
        IsStunned = false;
    }
}
