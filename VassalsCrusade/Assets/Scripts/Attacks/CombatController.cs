using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{

    Physical,
    Magical,
    Off

}

public class CombatController : MonoBehaviour
{
    Transform attackPoint;
    float angle;
    float distance = 2;

    Vector3 mousePos;

    public Texture2D characterAttackCursor;
    public Texture2D characterMagicCursor;
    public Texture2D characterHandORSomethingCursor;// For menus? NPC talk?

    PlayerController playerController;

    public delegate void CursorChange(AttackType pam);
    public static event CursorChange CombatChange;

    public AttackType attackStance;

    public AttackScriptableObject[] attacksArsenal;

    [HideInInspector]
    public string currentAttack;
    string lastPhysicalAttack;
    string lastMagicAttack;


    void Start()
    {
        attackStance = AttackType.Physical;

        playerController = GetComponent<PlayerController>();
        attackPoint = transform.Find("AttackPoint");


        foreach (AttackScriptableObject aso in attacksArsenal)
        {

        }
        currentAttack = attacksArsenal[0].attackName; //slash 
        lastPhysicalAttack = currentAttack;
        lastMagicAttack = attacksArsenal[1].attackName; //typhoon

    }

    void Update()
    {
        // If not on dialogue can changue attack mode
        if (Input.GetKeyDown(KeyCode.Space) &&
            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dialogue"))
        {
            ToogleAtackingMode();
            CombatChange?.Invoke(attackStance);
        }
    }

    void FixedUpdate()
    {
        // For attack point
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos -= transform.position;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (angle < 0.0f) angle += 360f;

        //transform.localEulerAngles = new Vector3(0, 0, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

        attackPoint.localPosition = new Vector3(xPos, yPos, 0);
    }


    public void Attack(string attack, float cosmic)
    {
        AttackScriptableObject aso = Array.Find(attacksArsenal, attackInArsenal => attackInArsenal.attackName == attack);

        if (aso == null)
        {
            Debug.Log("Attack Not Found");
            return;
        }

        //Ways to instantiate attacks
        switch (attack)
        {
            case "Slash":
                Instantiate(aso.prefab, attackPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
                break;
            case "Typhoon":
                if (cosmic >= 10)
                {
                    playerController.TakeCosmic(10);
                    Instantiate(aso.prefab, transform.position, transform.rotation);
                }
                break;
            default:
                break;
        }

    }

    public void ToogleAtackingMode()
    {
        switch (attackStance)
        {
            case AttackType.Physical:
                {
                    attackStance = AttackType.Magical;
                    currentAttack = lastMagicAttack;
                    break;
                }
            case AttackType.Magical:
                {
                    attackStance = AttackType.Physical;
                    currentAttack = lastPhysicalAttack;
                    break;
                }
            default: break; //Maybe dialogue or menu
        }
    }
}
