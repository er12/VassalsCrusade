using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCursorMode
{

    Physical,
    Magical,
    Off

}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxCosmic = 100f;
    public float currentCosmic;
    bool startedGainingCosmicPeriodically;
    public StatusBarController statusBar;


    [SerializeField]
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

    [HideInInspector]
    public Animator animator;

    static bool playerExists;


    List<string> availableAttacks;
    public string CurrentAttack { get; set; }


    public PlayerCursorMode playerCursorMode;
    public delegate void CursorChange(PlayerCursorMode pam);
    public static event CursorChange CombatChange;

    public delegate void PlayerStatus(float value);
    public static event PlayerStatus CosmicUpdate;

    // Start is called before the first frame update
    void Start()
    {
        if (!playerExists)
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

        playerCursorMode = PlayerCursorMode.Physical;

        availableAttacks = new List<string>()
        {
            "Slash"
        };
        CurrentAttack = "";

        // Status Bar
        currentHealth = maxHealth;
        currentCosmic = maxCosmic;
        FindObjectOfType<StatusBarController>().SetMaxHealth(maxHealth);
        FindObjectOfType<StatusBarController>().SetMaxCosmic(maxCosmic);



        TransitionToState(WalkingState);
    }

    void Update()
    {
        currentState.Update(this);

        // If not on dialogue can changue attack mode
        if (Input.GetKeyDown(KeyCode.Space) &&
            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dialogue"))
        {
            ToogleAtackingMode();
            CombatChange?.Invoke(playerCursorMode);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(20);
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
        switch (playerCursorMode)
        {
            case PlayerCursorMode.Physical:
                {
                    playerCursorMode = PlayerCursorMode.Magical;
                    break;
                }
            case PlayerCursorMode.Magical:
                {
                    playerCursorMode = PlayerCursorMode.Physical;
                    break;
                }
            default: break; //Maybe dialogue or menu
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        statusBar.SetHealth(currentHealth);
    }
    public void TakeCosmic(float cosmic)
    {
        currentCosmic -= cosmic;
        CosmicUpdate?.Invoke(currentCosmic);

        if (!startedGainingCosmicPeriodically)
        {
            startedGainingCosmicPeriodically = true;
            StartCoroutine(GainCosmicPeriodically());
        }
    }

    public void GainCosmic(float cosmic)
    {
        currentCosmic += cosmic;
        CosmicUpdate?.Invoke(currentCosmic);
    }

    IEnumerator GainCosmicPeriodically()
    {
        while (currentCosmic < maxCosmic)
        {
            GainCosmic(5f);
            yield return new WaitForSeconds(1f);
        }
        startedGainingCosmicPeriodically = false;
    }
}
