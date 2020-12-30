using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerBaseState
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
            player.Animator.SetFloat("Horizontal", movement.x);
            player.Animator.SetFloat("Vertical", movement.y);
            player.Animator.SetFloat("Speed", movement.sqrMagnitude);
            player.Animator.SetBool("Walking", true);

        }
        else
        {
            player.Animator.SetBool("Walking", false);
        }


        //make dodge only when running (TODO)
        if (Input.GetKeyDown(KeyCode.F) && movement.magnitude!= 0)
        {
            player.TransitionToState(player.DodgingState);
        }
    }

    public override void FixUpdate(PlayerController player)
    {
        player.Rigidbody.MovePosition(player.Rigidbody.position + movement * player.moveSpeed * Time.deltaTime);
    }
}
