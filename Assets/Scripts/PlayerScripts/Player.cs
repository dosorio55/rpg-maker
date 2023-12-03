using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator Animator;
    public PlayerStateMachine StateMachine { get; private set; }
    public Rigidbody2D RigidBody { get; private set; }
    public Transform Transform { get; private set; }
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
    public int FacingDirection = 1;

    [Header("Wall Slide Properties")]
    [SerializeField] public float WallSlideSpeed = 0.5f;
    [SerializeField] public float WallSlide;
    public bool IsWallSliding { get; set; }

    [Header("Dash Properties")]

    [SerializeField] public float DashSpeed = 20f;
    private bool ableToDash = true;
    [SerializeField] public float DashDuration = 0.2f;
    [SerializeField] public float DashCooldown = 0.8f;
    public int DashDirection { get; private set; } = 1;

    #region States
    public MoveState MoveState { get; private set; }
    public IdleState IdleState { get; private set; }
    public AirState AirState { get; private set; }
    public DashState DashState { get; private set; }
    public WallSlideState WallSlideState { get; private set; }
    public AttackState AttackState { get; private set; }
    #endregion

    public void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(this, StateMachine, "isIdle");
        MoveState = new MoveState(this, StateMachine, "isMoving");
        AirState = new AirState(this, StateMachine, "isJumping");
        DashState = new DashState(this, StateMachine, "isDashing");
        WallSlideState = new WallSlideState(this, StateMachine, "isWallSliding");
        AttackState = new AttackState(this, StateMachine, "isAttacking");
    }

    public void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
        Animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        StateMachine.CurrentState.Update();
        CheckDashInput();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        RigidBody.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && ableToDash)
        {
            if (IsWallSliding)
                Flip();
            ableToDash = false;
            int xInput = (int)Input.GetAxisRaw("Horizontal");
            DashDirection = xInput == 0 ? FacingDirection : xInput;
            StartCoroutine(StartTimer(() => ableToDash = true, DashCooldown));
            StateMachine.ChangeState(DashState);
        }
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer);

    protected void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && FacingDirection != 1)
        {
            Flip();
        }
        else if (_x < 0 && FacingDirection == 1)
        {
            Flip();
        }
    }

    public IEnumerator StartTimer(System.Action _endTimerAction, float _time)
    {
        yield return new WaitForSeconds(_time);
        _endTimerAction();
    }

    public void StopTimer(Coroutine _coroutine)
    {
        StopCoroutine(_coroutine);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // line to check if the player is grounded
        Gizmos.DrawLine(
            groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0)
        );

        Gizmos.color = Color.blue;
        // line to check if the player is touching a wall
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * FacingDirection, wallCheck.position.y));
    }
}
