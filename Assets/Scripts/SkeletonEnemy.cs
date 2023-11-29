using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : CharacterBase
{
    [Header("Movement")] //================================================
    [SerializeField] private float _moveSpeed = 3f;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Movement();

        if (!_isGrounded || _isWallDetected)
        {
            Flip();
        }
    }

    private void Movement()
    {
        _rigidBody.velocity = new Vector2(_moveSpeed * _facingDirection, _rigidBody.velocity.y);

    }
}
