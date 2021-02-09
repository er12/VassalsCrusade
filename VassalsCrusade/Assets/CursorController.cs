using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Vector3 mousePosition;
    SpriteRenderer spriteRenderer;

    public Transform player; 
    Texture2D currentPlayerCursorMagicSprite;
    Texture2D currentPlayerCursorAttackSprite;     

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCursorMode(FindObjectOfType<PlayerController>().playerCursorMode);

        LoadPlayerCursors();
    }

    // TO DO: CHange Ziv and zenon
    void LoadPlayerCursors()
    {
        CombatController cc = FindObjectOfType<PlayerController>().GetComponent<CombatController>();

        currentPlayerCursorAttackSprite = cc.characterAttackCursor;
        currentPlayerCursorMagicSprite = cc.characterMagicCursor;        
        //For current spritesheet 
        spriteRenderer.flipY = true;
    }

    void SetCursorMode(PlayerCursorMode playerCursorMode)
    {
        switch(playerCursorMode)
        {
            case PlayerCursorMode.Physical:
            {
                Cursor.SetCursor(currentPlayerCursorAttackSprite, hotSpot,cursorMode);
                break;
            }
            case PlayerCursorMode.Magical:
            {
                Cursor.SetCursor(currentPlayerCursorMagicSprite, hotSpot,cursorMode);
                break;
            }
            default: break; //Maybe dialogue or menu
        }
    }

    
    void OnEnable()
    {
        PlayerController.CombatChange += SetCursorMode;
    }


    void OnDisable()
    {
        PlayerController.CombatChange -= SetCursorMode;
    }
}
