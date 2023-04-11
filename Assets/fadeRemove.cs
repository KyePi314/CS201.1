//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class fadeRemove : StateMachineBehaviour
//{
//    public float timer = 0.5f;

//    private float timeElapsed = 0;
//    EnemyLevels enemyLevels;
//    SpriteRenderer spriteRenderer;
//    GameObject objRemoved;
//    Color startColor;

//    OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
//    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//    {
//        timeElapsed = 0f;
//        spriteRenderer = animator.GetComponent<SpriteRenderer>();
//        startColor = spriteRenderer.color;
//        objRemoved = animator.gameObject;
//    }

//    OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//    {
//        timeElapsed += Time.deltaTime;
//        float newAlpha = startColor.a * (1 - (timeElapsed / timer));
//        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

//        if (timeElapsed > timer)
//        {
//            enemyLevels.deathXP();
//            Destroy(objRemoved);
//        }
//    }

//}
