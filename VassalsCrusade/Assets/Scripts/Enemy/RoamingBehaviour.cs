using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class RoamingBehaviour : StateMachineBehaviour
{
    EnemyController enemyController;
    Transform target;
    private Rigidbody2D rigidbody;
    private Vector3 startPosition;
    private Vector3 roamingPosition;
    public float moveSpeed = 1f;
    private float reachedPositionDistance = 1f;
    private float roamingRadius = 5f;
    AIPath aiPath;

    PolygonCollider2D worldCollider;
    bool roamToStartPoint;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.transform.parent.GetComponent<EnemyController>();
        aiPath = enemyController.GetComponent<AIPath>();
        aiPath.enabled = false;
        rigidbody = enemyController.GetComponent<Rigidbody2D>();

        startPosition = rigidbody.position;
        target = enemyController.target;
        worldCollider = GameObject.Find("World").GetComponent<PolygonCollider2D>();
        roamingPosition = RandomPointOnAreaInRadius(rigidbody.position, roamingRadius);
        roamToStartPoint = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Vector3 movePosition = rigidbody.position;

        //Roaming
        movePosition.x = Mathf.MoveTowards(rigidbody.position.x, roamingPosition.x, moveSpeed * Time.deltaTime);
        movePosition.y = Mathf.MoveTowards(rigidbody.position.y, roamingPosition.y, moveSpeed * Time.deltaTime);

        animator.SetFloat("movementX", (roamingPosition.x - rigidbody.position.x));
        animator.SetFloat("movementY", (roamingPosition.y - rigidbody.position.y));


        rigidbody.MovePosition(movePosition);

        if (Vector3.Distance(rigidbody.position, roamingPosition) <= reachedPositionDistance) // Reached Roam Position
        {
            if (roamToStartPoint)
            {
                roamToStartPoint = false;
                roamingPosition = RandomPointOnAreaInRadius(animator.transform.position, roamingRadius);
            }
            else
            {
                roamToStartPoint = true;
                roamingPosition = startPosition; // Go Back
            }
        }

        //Debug.Log(Vector3.Distance(rigidbody.position, target.position) + "///" + rigidbody.position); // radius of noticing player
        if (Vector3.Distance(rigidbody.position, target.position) <= 7.5f) // radius of noticing player
            animator.SetBool("isAware", true);

    }

    Vector3 RandomPointOnAreaInRadius(Vector3 origin, float distance)
    {
        float minX = worldCollider.bounds.min.x,
            maxX = worldCollider.bounds.max.x,
            minY = worldCollider.bounds.min.y,
            maxY = worldCollider.bounds.max.y;

        float originMinX = origin.x - distance,
              originMaxX = origin.x + distance;
        float originMinY = origin.y - distance,
              originMaxY = origin.y + distance;


        Vector3 randomPlaceWithinRange = new Vector2(
            Random.Range(greatest(minX, originMinX), smallest(maxX, originMaxX)),
            Random.Range(greatest(minY, originMinY), smallest(maxY, originMaxY))
            );

        return randomPlaceWithinRange;
    }

    float smallest(float a, float b)
    {
        return (a < b ? a : b);
    }

    float greatest(float a, float b)
    {
        return (a > b ? a : b);
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
