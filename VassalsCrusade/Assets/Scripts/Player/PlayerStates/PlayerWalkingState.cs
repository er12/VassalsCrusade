using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    private Vector2 movement;
    public Vector2 Movement { get { return movement; } }

    public override void EnterState(PlayerController player)
    {
        //throw new System.NotImplementedException();
    }

    public override void Update(PlayerController player)
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            player.animator.SetFloat("Horizontal", movement.x);
            player.animator.SetFloat("Vertical", movement.y);
            player.animator.SetFloat("Speed", movement.sqrMagnitude);
            player.animator.SetBool("Walking", true);
        }
        else
            player.animator.SetBool("Walking", false);



        // Dodge only when running 
        if (Input.GetMouseButtonDown(1) && movement.magnitude != 0)
        {
            player.TransitionToState(player.DodgingState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.TransitionToState(player.AttackingState);
        }
    }

    public override void FixUpdate(PlayerController player)
    {
        player.Rigidbody.MovePosition(player.Rigidbody.position + movement * player.moveSpeed * Time.deltaTime);
    }
}
