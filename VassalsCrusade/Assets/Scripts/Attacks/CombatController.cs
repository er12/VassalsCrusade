using System;
using UnityEngine;

public enum AttackType
{

    Physical,
    Magical,
    Status

}

public class CombatController : MonoBehaviour
{
    Transform attackPoint;

    public float angle;
    float distance = 2;

    Vector3 mousePos;
    [HideInInspector]
    public Vector3 directionVector;

    public Texture2D characterAttackCursor;
    public Texture2D characterMagicCursor;
    public Texture2D characterHandORSomethingCursor;// For menus? NPC talk?

    PlayerController playerController;

    public delegate void CursorChange(AttackType pam);
    public static event CursorChange CombatChange;

    public AttackType attackStance;

    public AttackScriptableObject physicalAttack;
    public AttackScriptableObject[] magicAttacksArsenal;
    string currentMagicAttack;

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

        //hardcoded typhoon
        currentMagicAttack = magicAttacksArsenal[0].attackName;

        magicMenu = FindObjectOfType<MagicMenu>();
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
            //For selecting magic
            if (showingMagicMenu)
            {
                // if (angle <=

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

        attackPoint.localEulerAngles = new Vector3(0, 0, angle);
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

        directionVector = new Vector3(xPos, yPos, 0);
        attackPoint.localPosition = directionVector;
    }

    public void Attack()
    {
        if (attackStance == AttackType.Physical)
        {
            // Make more physical attacks
            // Short range long range
            Instantiate(physicalAttack.prefab, attackPoint.position , Quaternion.Euler(new Vector3(0, 0, angle + 90)));  // +90 to rotate
            return;
        }

        //TODO make current magic attack a scriptable object
        AttackScriptableObject aso = Array.Find(magicAttacksArsenal, attackInArsenal => attackInArsenal.attackName == currentMagicAttack);

        if (aso == null)
        {
            Debug.Log("Magic Not Found");
            return;
        }

        if (playerController.currentCosmic >= aso.cosmicNeeded)
        {
            playerController.TakeCosmic(aso.cosmicNeeded);

            switch (currentMagicAttack)
            {
                case "Typhoon":
                    Instantiate(aso.prefab, transform.position, transform.rotation);
                    break;
                case "PushingWind":
                    GameObject wind = Instantiate(aso.prefab, attackPoint.position, attackPoint.rotation);
                    if (angle >= 90 && angle <= 270)
                    {
                        wind.GetComponent<SpriteRenderer>().flipY = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void ToogleAtackingMode()
    {
        switch (attackStance)
        {
            case AttackType.Physical:
                {
                    attackStance = AttackType.Magical;
                    break;
                }
            case AttackType.Magical:
                {
                    attackStance = AttackType.Physical;
                    break;
                }
            default: break; //Maybe dialogue or menu
        }
        CombatChange?.Invoke(attackStance);
    }

    void currentMagicChange(string name)
    {
        currentMagicAttack = name;
        attackStance = AttackType.Magical;
        CombatChange?.Invoke(attackStance);
    }

    void OnEnable()
    {
        MagicMenu.MagicSelected += currentMagicChange;
    }

    void OnDisable()
    {
        MagicMenu.MagicSelected -= currentMagicChange;
    }
}
