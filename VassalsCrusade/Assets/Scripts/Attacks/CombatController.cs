using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject slashPrefab;
    public Transform attackPoint;

    Vector2 mousePos;
    
    public Texture2D characterAttackCursor;
    public Sprite characterMagicCursor;
    public Sprite characterHandORSomethingCursor;


    private void Update()
    {

    }

    public void SpawnSlash()
    {

        Vector2 attackPosition = attackPoint.transform.position;
        
        Instantiate(slashPrefab, attackPosition, transform.rotation);

    }

}
