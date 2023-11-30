using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonEnemy : CharacterBase
{
    [Header("Movement")] //================================================
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private LayerMask _playerLayer;

    private RaycastHit2D _isPlayerDetected;
    [SerializeField] private float _playerDetectionDistance = 5f;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        CheckPlayerCollition();
        Movement();

        if (!_isGrounded || _isWallDetected)
        {
            Flip();
        }
    }

    private void Movement()
    {
        Debug.Log(_isPlayerDetected);
        float actualSpeed = _isPlayerDetected ? _moveSpeed + 4.4f : _moveSpeed;
        _rigidBody.velocity = new Vector2(actualSpeed * _facingDirection, _rigidBody.velocity.y);
    }

    protected void CheckPlayerCollition()
    {
        _isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * _facingDirection, _playerDetectionDistance, _playerLayer);
    }

    override protected void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position,
            new Vector3(transform.position.x + _playerDetectionDistance * _facingDirection, transform.position.y, 0)
        );
    }
}
