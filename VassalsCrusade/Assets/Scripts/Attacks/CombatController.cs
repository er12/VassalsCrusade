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

    public AttackScriptableObject physicalAttack;
    public AttackScriptableObject[] magicAttacksArsenal;

    [HideInInspector]
    public string currentAttack;
    string lastPhysicalAttack;
    string lastMagicAttack;

    MagicMenu magicMenu;
    float spaceHoldTimer;
    bool spaceHold;
    bool showingMagicMenu;
    bool isSinglePress;


    void Start()
    {
        attackStance = AttackType.Physical;

        playerController = GetComponent<PlayerController>();
        attackPoint = transform.Find("AttackPoint");


        foreach (AttackScriptableObject aso in magicAttacksArsenal)
        {

        }
        currentAttack = magicAttacksArsenal[0].attackName; //typhoon 
        lastPhysicalAttack = currentAttack;

        magicMenu = FindObjectOfType<MagicMenu>();
        magicMenu.gameObject.SetActive(false);
        spaceHoldTimer = 0;

    }

    void Update()
    {
        //TODO: disable combat controller on dialogue

        //Magic menu legic, to make it appear when on hold and just change attack mode on single press
        spaceHold = Input.GetKey(KeyCode.Space);

        if (!spaceHold && isSinglePress && !showingMagicMenu)
        {
            ToogleAtackingMode();
            CombatChange?.Invoke(attackStance);
        }

        isSinglePress = spaceHold; // record for next frame

        if (spaceHold)
        {
            spaceHoldTimer += Time.deltaTime;

            if (spaceHoldTimer > 0.5f && !showingMagicMenu)
            {
                showingMagicMenu = true;
                magicMenu.ToogleMagicMenu();
            }
        }
        else
        {
            spaceHoldTimer = 0;
            if (showingMagicMenu)
            {
                spaceHoldTimer = 0;
                magicMenu.ToogleMagicMenu();
                showingMagicMenu = false;
            }
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
        if (attackStance == AttackType.Physical)
        {
            // make more physical attacks
            Instantiate(physicalAttack.prefab, attackPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            return;
        }

        AttackScriptableObject aso = Array.Find(magicAttacksArsenal, attackInArsenal => attackInArsenal.attackName == attack);

        if (aso == null)
        {
            Debug.Log("Magic Not Found");
            return;
        }

        //Ways to instantiate magic

        switch (attack)
        {
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
