using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
//    public Transform target;
//    enemyMovement speed;
//    Rigidbody2D rigidbody;
//    enemyMovement movement;
//    // Start is called before the first frame update
//    void Start()
//    {
//        rigidbody = GetComponent<Rigidbody2D>();
//        if (target == null)
//        {
//            if (GameObject.FindWithTag("Player") != null)
//            {
//                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void Follow()
//    {
//        Vector2 playerPos = Vector2.MoveTowards(transform.position, target.position, speed.walkSpeed * Time.deltaTime);
//        rigidbody.MovePosition(playerPos);
//        transform.LookAt(playerPos);
//    }

//    private void OnTriggerStay2D(Collider2D player)
//    {
//        if (movement.seesTarget == true && player.tag == "Player")
//        {

//            Follow();
//        }
//    }
}
