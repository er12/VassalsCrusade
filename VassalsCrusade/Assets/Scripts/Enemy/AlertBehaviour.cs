using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehaviour : StateMachineBehaviour
{
    private Transform targetTransform;

    private Rigidbody2D rbody;
    private Vector3 movement;

    public float speed = 8;
    public float stoppingDistance = 3f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rbody = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(animator.transform.position, targetTransform.position) > stoppingDistance )
        {
            movement = Vector2.MoveTowards(animator.transform.position, targetTransform.position, speed * Time.deltaTime);
            animator.transform.position = movement;

        }
        animator.SetFloat("movementY", (targetTransform.position.y - animator.transform.position.y));
        animator.SetFloat("movementX", (targetTransform.position.x - animator.transform.position.x));
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
