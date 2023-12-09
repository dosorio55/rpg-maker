using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Rigidbody2D RigidBody { get; private set; }
    public Animator Animator;
    public Transform Transform { get; private set; }
    #endregion

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;
    public int FacingDirection = 1;

    protected virtual void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        RigidBody.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    public Coroutine SetTimer(System.Action _endTimerAction, float _time)
    {
       return StartCoroutine(StartTimer(_endTimerAction, _time));
    }

    private IEnumerator StartTimer(System.Action _endTimerAction, float _time)
    {
        yield return new WaitForSeconds(_time);
        _endTimerAction();
    }

    public void StopTimer(Coroutine _coroutine)
    {
        StopCoroutine(_coroutine);
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

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, wallCheckDistance, groundLayer);

    public virtual void StopMoving()
    {
        SetVelocity(0, RigidBody.velocity.y);
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