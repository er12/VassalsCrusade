using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    public PlayerController player;

    private Vector2 movement;
    public Vector2 Movement { get { return movement; } }

    public float slashMoveVelocity = 4f;
    Vector3 slashDirection;



    public CombatController shortRangeAttack;

    public override void EnterState(PlayerController player)
    {
        this.player = player;

        shortRangeAttack = player.GetComponent<CombatController>();

        slashMoveVelocity = 4f;
        slashDirection = player.WalkingState.Movement;

        //Play animation
        player.Animator.SetTrigger("Attack");
        Attack(player.CurrentAttack);

    }

    public override void Update(PlayerController player)
    {

       //Transition back to walking
       if (!checkIsAttacking(player.Animator))
            player.TransitionToState(player.WalkingState);
    }

    public override void FixUpdate(PlayerController player)
    {
        
        // If slash, move a litte forward
        player.Rigidbody.velocity = slashDirection * slashMoveVelocity;
    }


    // TODO: USE EVENTS to fix this , note it has 4 animations
    public bool checkIsAttacking(Animator anim)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            return true;
        }
        return false;
    }

    void Attack(string attack)
    {

        switch (attack)
        {
            case "Slash":
                shortRangeAttack.SpawnSlash();
                break;
            default:
                break;
        }
    }
}
