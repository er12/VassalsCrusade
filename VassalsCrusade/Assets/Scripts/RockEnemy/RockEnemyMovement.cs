using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemyMovement : MonoBehaviour
{
    public EnemyController enemyController;
    Vector2 movePosition = new Vector2();
    Rigidbody2D rigidbody;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = enemyController.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //animator.SetFloat("MovementX", 1);

    }

    void Step()
    {
        movePosition.x = Mathf.MoveTowards(rigidbody.position.x, 10, 25 * Time.deltaTime);
        rigidbody.MovePosition(movePosition);
    }
}
