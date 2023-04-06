using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeRemove : StateMachineBehaviour
{
    public float timer = 0.5f;

    private float timeElapsed = 0;

    SpriteRenderer spriteRenderer;
    GameObject objRemoved;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objRemoved = animator.gameObject;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;


    }
}
