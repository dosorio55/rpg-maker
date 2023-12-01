using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator Animator;
    public PlayerStateMachine StateMachine { get; private set; }
    public Rigidbody2D RigidBody { get; private set; }
    #endregion

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;

    [Header("Player Properties")]
    [SerializeField] public float MoveSpeed = 7;
    [SerializeField] public float JumpForce = 10;


    #region States
    public MoveState MoveState { get; private set; }
    public IdleState IdleState { get; private set; }
    public JumpState JumpState { get; private set; }
    #endregion

    public void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(this, StateMachine, "isIdle");
        MoveState = new MoveState(this, StateMachine, "isMoving");
        JumpState = new JumpState(this, StateMachine, "isJumping");
    }

    public void Start()
    {
        StateMachine.Initialize(IdleState);
        Animator = GetComponentInChildren<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        StateMachine.CurrentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        RigidBody.velocity = new Vector2(_xVelocity, _yVelocity);
    }
                                    //   Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer);
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0)
        );

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
