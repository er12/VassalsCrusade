using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private Vector3 dodgeDirection;
    public float dodgeSpeed;

    public override void EnterState(PlayerController player)
    {
        dodgeDirection = player.WalkingState.Movement;
        dodgeSpeed = 35f;

        player.Animator.SetBool("Dodging", true);
    }

    public override void Update(PlayerController player)
    {

        float dodgeSpeedDropMultiplier = 7f;
        dodgeSpeed -= dodgeSpeed * dodgeSpeedDropMultiplier * Time.deltaTime;

        float dodgeSpeedMinimun = 7f;

        //Finished dodgeing
        if (dodgeSpeed < dodgeSpeedMinimun)
        {
            player.Animator.SetBool("Dodging", false);
            player.TransitionToState(player.WalkingState);
        }

    }

    public override void FixUpdate(PlayerController player)
    {
        player.Rigidbody.velocity = dodgeDirection * dodgeSpeed;
    }
}

