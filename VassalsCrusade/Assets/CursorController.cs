using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Vector3 mousePosition;
    SpriteRenderer spriteRenderer;
    Sprite currentPlayerCursorMagicSprite;
    Texture2D currentPlayerCursorAttackSprite;     

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadPlayerCursors();
        SetCursorMode("Attack");
    }

    // TO DO: CHange Ziv and zenon
    void LoadPlayerCursors()
    {
        currentPlayerCursorAttackSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatController>().characterAttackCursor;
        currentPlayerCursorMagicSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatController>().characterMagicCursor;        
        //For current spritesheet 
        spriteRenderer.flipY = true;
    }

    void SetCursorMode(string mode)
    {
        switch(mode)
        {
            case "Attack":
                Cursor.SetCursor(currentPlayerCursorAttackSprite, hotSpot,cursorMode);
                break;
            case "Magic":
                //spriteRenderer.sprite = currentPlayerCursorMagicSprite;
                break;
            default:
                //spriteRenderer.sprite = currentPlayerCursorAttackSprite;
                break;
        }

    }
}
