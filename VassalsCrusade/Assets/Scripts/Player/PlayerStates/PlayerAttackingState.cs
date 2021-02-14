using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    public PlayerController player;
    private Vector2 movement;
    public Vector2 Movement { get { return movement; } }

    public float attackStepVelocity = 1f;
    Vector3 slashDirection;

    Vector3 mousePosition;
    Vector3 posDif;
    public CombatController combatController;


    public override void EnterState(PlayerController player)
    {
        this.player = player;
        combatController = player.GetComponent<CombatController>();


        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posDif = mousePosition - player.transform.position;

        slashDirection = posDif;

        //To make it stay in direction facing
        player.animator.SetFloat("Horizontal", posDif.x);
        player.animator.SetFloat("Vertical", posDif.y);

        //Animator transition to attacking
        player.animator.SetTrigger("Attack");
        player.animator.SetFloat("Mouse Horizontal", posDif.x);
        player.animator.SetFloat("Mouse Vertical", posDif.y);

        //Play animation
        combatController.Attack(combatController.currentAttack, player.currentCosmic);

    }

    public override void Update(PlayerController player)
    {

        // TODO: maybe USE animations EVENTS to fix this , note it has 4 animations
        //Transition back to walking if not attacking 
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
            player.TransitionToState(player.WalkingState);
    }

    public override void FixUpdate(PlayerController player)
    {
        // If attack, move a litte forward
        player.Rigidbody.velocity = posDif.normalized * attackStepVelocity;
    }



}
