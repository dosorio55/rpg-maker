using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : CharacterBase
{
    // private Animator _animator;

    [Header("Movement")] //================================================
    // [SerializeField] private float _moveSpeed = 7;
    private float _horizontalInput;
    // [SerializeField] private float _jumpForce = 10;
    private bool _isMoving = false;

    [Header("Attack")] //================================================
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private int _comboCount = 0;
    [SerializeField] private float _comboAttackWindow = 0.5f;
    private float _comboAttackTimer = 0f;

    [Header("Dash")] //================================================
    [SerializeField] private float _dashDuration = 0.3f;
    // [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private float _dashCooldown = 0.4f;
    [SerializeField] private bool _dashOnCooldown = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        CheckCoolDown();
        CheckInput();
        Movement();

        AnimatorControllers();

        _comboAttackTimer -= Time.deltaTime;
    }

    private IEnumerator Dash()
    {
        _isDashing = true;

        yield return new WaitForSeconds(_dashDuration);

        _isDashing = false;
        _dashOnCooldown = true;
    }

    private void CheckCoolDown()
    {
        if (_dashOnCooldown)
        {
            _dashCooldown -= Time.deltaTime;
            if (_dashCooldown <= 0)
            {
                _dashCooldown = 0.4f;
                _dashOnCooldown = false;
            }
        }
    }

    private void AnimatorControllers()
    {
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetBool("isDashing", _isDashing);
        _animator.SetBool("isAttacking", _isAttacking);
        _animator.SetInteger("comboCounter", _comboCount);
    }

    public void AttackAnimationFinished()
    {
        _comboCount++;
        _isAttacking = false;

        if (_comboCount == 3 || _comboAttackTimer < 0)
        {
            _comboCount = 0;
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isGrounded)
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_dashOnCooldown && !_isDashing && !_isAttacking)
        {
            StartCoroutine(Dash());
        }

        _horizontalInput = Input.GetAxis("Horizontal");
    }

    private void StartAttackEvent()
    {
        _isAttacking = true;
        _comboAttackTimer = _comboAttackWindow;
    }

    private void Movement()
    {
        // if (_isAttacking)
        // {
        //     _rigidBody.velocity = new Vector2(0, 0);
        // }
        // else if (_isDashing)
        // {
        //     _rigidBody.velocity = new Vector2(_facingDirection * _dashSpeed, 0);
        // }
        // else
        // {
        //     _rigidBody.velocity = new Vector2(_horizontalInput * _moveSpeed, _rigidBody.velocity.y);
        // }


        if (_horizontalInput > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_horizontalInput < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        // if (_isGrounded)
        // {
            // _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        // }
    }

    void FixedUpdate()
    {
        #region Move with MovePosition -- commented out
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");
        // Vector3 moveInput = new Vector3(horizontalInput, verticalInput, 0);

        // Vector3 newPos = transform.position + moveInput * Time.deltaTime * _moveSpeed;
        // _rigidBody.MovePosition(newPos);
        #endregion
    }
}
