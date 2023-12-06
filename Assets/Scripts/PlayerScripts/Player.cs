using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public PlayerStateMachine StateMachine { get; private set; }

    [Header("Player Properties")]
    [SerializeField] public float MoveSpeed = 7;
    [SerializeField] public float JumpForce = 10;


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

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(this, StateMachine, "isIdle");
        MoveState = new MoveState(this, StateMachine, "isMoving");
        AirState = new AirState(this, StateMachine, "isJumping");
        DashState = new DashState(this, StateMachine, "isDashing");
        WallSlideState = new WallSlideState(this, StateMachine, "isWallSliding");
        AttackState = new AttackState(this, StateMachine, "isAttacking");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
        CheckDashInput();
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
}
