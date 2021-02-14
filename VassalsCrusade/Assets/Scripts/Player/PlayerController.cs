using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxCosmic = 100f;
    public float currentCosmic;
    bool startedGainingCosmicPeriodically;


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

    public delegate void PlayerStatus(float value);
    public static event PlayerStatus CosmicUpdate;
    public static event PlayerStatus HealthUpdate;


    // Start is called before the first frame update
    void Start()
    {
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Status Bar
        currentHealth = maxHealth;
        currentCosmic = maxCosmic;
        FindObjectOfType<StatusBarController>().SetMaxHealth(maxHealth);
        FindObjectOfType<StatusBarController>().SetMaxCosmic(maxCosmic);

        TransitionToState(WalkingState);
    }

    void Update()
    {
        currentState?.Update(this);

        if (CurrentState == null)
            TransitionToState(WalkingState);

        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(20);
        }
    }

    void FixedUpdate()
    {
        currentState?.FixUpdate(this);
    }

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthUpdate?.Invoke(currentHealth);
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
