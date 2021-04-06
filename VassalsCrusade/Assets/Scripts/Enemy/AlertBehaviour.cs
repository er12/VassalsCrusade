using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AlertBehaviour : StateMachineBehaviour
{
    private Transform targetTransform;

    private Rigidbody2D rbody;
    private Vector3 movement;

    public float stoppingDistance = 2f;

    AIPath aiPath;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        aiPath = animator.GetComponentInParent<AIPath>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, targetTransform.position) <= stoppingDistance) // radius for atacking player
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            if (!aiPath.enabled)
                aiPath.enabled = true;

            animator.SetFloat("movementX", aiPath.desiredVelocity.x);
            animator.SetFloat("movementY", aiPath.desiredVelocity.y);

            if (Vector3.Distance(animator.transform.position, targetTransform.position) >= 15f) // radius for forgetting player
                animator.SetBool("isAware", false);
        }
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
