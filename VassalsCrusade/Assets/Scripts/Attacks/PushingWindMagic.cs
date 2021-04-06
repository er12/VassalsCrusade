using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PushingWindMagic : MonoBehaviour
{
    CombatController combatController;
    public AttackType attack = AttackType.Status;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    Element element = Element.Air;

    void Start()
    {
        combatController = GameObject.FindObjectOfType<CombatController>();

        //Destroy after 1 second
        Destroy(gameObject, 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //TODO:make tag chemistry system
        if (col.gameObject.name.Contains("Plant Fog"))
        {
            col.GetComponent<PlantFog>().Dispel(col.gameObject.transform, combatController.directionVector.normalized, smoothTime, velocity);
        }

        //If want to push enemy 
        /*
        if (col.gameObject.tag == "Enemy" && false)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();

            // if ground/metal monster, do not push
            if (enemyController.IsAffected(this.element))
            {
                StartCoroutine(enemyController.PushBack(col.gameObject.transform, combatController.directionVector.normalized, smoothTime, velocity));
            }
        }*/
    }

}
