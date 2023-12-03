using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
 
    protected Animator _animator;

    [Header("Collition Information")] //================================================
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected float _groundCheckDistance;
    [Space]
    [SerializeField] protected Transform _wallCheck;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _groundLayer;


    protected bool _isGrounded = true;
    [SerializeField] protected bool _isWallDetected = false;


    [Header("Movement")] //================================================
    protected bool _facingRight = true;
    protected int _facingDirection = 1;

    protected virtual void Start()
    {
        // _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollisionCheck();
    }

    protected void CollisionCheck()
    {
        _isGrounded = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _groundLayer);
        if (_wallCheck != null)
            _isWallDetected = Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDirection, _wallCheckDistance, _groundLayer);
        //     _isWallDetected = Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDirection, _wallCheckDistance, _groundLayer);
    }

    protected void Flip()
    {
        _facingRight = !_facingRight;
        _facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            _groundCheck.position,
            new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance, 0)
        );

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
    }
}
