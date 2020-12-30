using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private PlayerBaseState currentState;
    public PlayerBaseState CurrentState
    {
        get { return currentState; }
    }
    public readonly PlayerIdleState IdleState = new PlayerIdleState();
    public readonly PlayerDodgingState DodgingState = new PlayerDodgingState();
    public readonly PlayerWalkingState WalkingState = new PlayerWalkingState();

    private Rigidbody2D rb;
    public Rigidbody2D Rigidbody
    {
        get { return rb; }
    }

    private Animator animator;
    public Animator Animator { get => animator;}

    static bool playerExists;

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

        TransitionToState(WalkingState);

    }

    // Update is called once per frame
    void Update()
    {

        currentState.Update(this);
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

}
