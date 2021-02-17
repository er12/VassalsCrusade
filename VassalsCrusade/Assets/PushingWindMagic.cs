using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingWindMagic : MonoBehaviour
{
    CombatController combatController;
    public AttackType attack = AttackType.Status;
    public float power = 10f;

    void Start()
    {
        combatController = GameObject.FindObjectOfType<CombatController>();

        //Destroy after 1 second
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.GetComponent<Rigidbody2D>().AddForce(combatController.directionVector.normalized * power);
        }
    }
}
