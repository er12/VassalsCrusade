using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackingMode{
    
    Physical,
    Magical,
    Off

}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public PlayerAttackingMode playerAttackingMode;

    private PlayerBaseState currentState;
    public PlayerBaseState CurrentState
    {
        get { return currentState; }
    }
    public readonly PlayerIdleState IdleState = new PlayerIdleState();
    public readonly PlayerDodgingState DodgingState = new PlayerDodgingState();
    public readonly PlayerMovingState WalkingState = new PlayerMovingState();
    public readonly PlayerAttackingState AttackingState = new PlayerAttackingState();

    private Rigidbody2D rb;
    public Rigidbody2D Rigidbody
    {
        get { return rb; }
    }

    private Animator animator;
    public Animator Animator { get => animator;}

    static bool playerExists;


    List<string> availableAttacks;
    public string CurrentAttack { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        playerAttackingMode = PlayerAttackingMode.Physical;

        availableAttacks= new List<string>()
        {
            "Slash"
        }
        ;
        CurrentAttack = "";

        TransitionToState(WalkingState);
    }

    void Update()
    {
        currentState.Update(this);

        // If not on dialogue can changue attack mode
        if(Input.GetKeyDown(KeyCode.Space) && 
            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dialogue"))
        {
            animator.GetComponent<PlayerController>().ToogleAtackingMode();

        }
    }

    void FixedUpdate()
    {
        currentState.FixUpdate(this);
    }

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void ToogleAtackingMode()
    {
        switch(playerAttackingMode)
        {
            case PlayerAttackingMode.Physical:
            {
                playerAttackingMode = PlayerAttackingMode.Magical;
                transform.Find("AttackPoint").gameObject.SetActive(false);
                transform.Find("MagicPoint").gameObject.SetActive(true);
                break;
            }
            case PlayerAttackingMode.Magical:
            {
                playerAttackingMode = PlayerAttackingMode.Physical;
                transform.Find("AttackPoint").gameObject.SetActive(true);
                transform.Find("MagicPoint").gameObject.SetActive(false);
                break;
            }
            default: break;
        }
    }
}
