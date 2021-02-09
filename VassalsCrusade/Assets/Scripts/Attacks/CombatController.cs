using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject slashPrefab;
    public GameObject typhoonPrefab;
    AttackPointController attackPoint;
    float angle;
    float distance = 2;

    Vector3 mousePos;

    public Texture2D characterAttackCursor;
    public Texture2D characterMagicCursor;
    public Texture2D characterHandORSomethingCursor;// For menus? NPC talk?
    public Camera myCamera;

    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        attackPoint = transform.Find("AttackPoint").GetComponent<AttackPointController>();

    }

    void FixedUpdate()
    {
        // For attack point
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        mousePos -= transform.position;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (angle < 0.0f) angle += 360f;

        //transform.localEulerAngles = new Vector3(0, 0, angle);

        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;

        attackPoint.transform.localPosition = new Vector3(xPos, yPos, 0);
    }


    public void Attack(string attack, float cosmic)
    {
        // TO DO: Change to scriptable object or something more fancy
        // And better use of slash prefabs
        switch (attack)
        {
            case "Slash":
                SpawnSlash();
                break;
            case "Typhoon":
                if (cosmic >= 10)
                {
                    playerController.TakeCosmic(10);
                    SpawnTyphoon();
                }
                break;
            default:
                break;

        }

    }

    public void SpawnSlash()
    {
        Instantiate(slashPrefab, attackPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }

    public void SpawnTyphoon()
    {
        Instantiate(typhoonPrefab, transform.position, transform.rotation);
    }

}
