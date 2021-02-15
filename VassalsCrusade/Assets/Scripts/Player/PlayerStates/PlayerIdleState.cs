using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private Vector2 movement;
    private Vector3 originalSize;
    private IEnumerator breathingCoroutine;


    public override void EnterState(PlayerController player)
    {
        player.Rigidbody.velocity = Vector2.zero; // attacking has some drift

        originalSize = player.transform.localScale;
        breathingCoroutine = SimulateBreathing(player.transform);
        // TODO: Idle animation, this breathing is nuts
        player.StartCoroutine(breathingCoroutine);
    }

    public override void Update(PlayerController player)
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            player.StopCoroutine(breathingCoroutine);
            player.transform.localScale = originalSize; //yeye ducplicate code

            player.TransitionToState(player.WalkingState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.StopCoroutine(breathingCoroutine);
            player.transform.localScale = originalSize; //yeye ducplicate code

            player.TransitionToState(player.AttackingState);
        }

        // TODO: Make Block state (guard)
        if (Input.GetMouseButtonDown(1))
        {
            //player.TransitionToState(player.DodgingState);
        }

    }

    public override void FixUpdate(PlayerController player) { }

    IEnumerator SimulateBreathing(Transform transform)
    {
        bool direction = true;
        float scaleFactor = 1;
        while (true)
        {
            for (float i = 0; i < 1; i += 0.1f)
            {
                if (direction)
                    scaleFactor += 0.003f;
                else
                    scaleFactor -= 0.003f;

                transform.localScale = Vector3.one * scaleFactor;

                yield return new WaitForSeconds(0.1f);
            }

            direction = !direction;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
