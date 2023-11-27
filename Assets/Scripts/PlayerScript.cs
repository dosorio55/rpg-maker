using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 7;

    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _jumpForce = 10;
    private Animator _animator;
    private bool _isMoving = false;
    private bool _facingRight = true;

    [Header("Dash")] //================================================
    [SerializeField] private float _dashDuration = 0.3f;
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private bool _isDashing = false;


    [Header("Ground Check")] //================================================
    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _groundCheckDistance;

    [SerializeField]
    private bool _isGrounded = true;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        AnimatorControllers();

        _isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            _groundCheckDistance,
            _groundLayer
        );


    }

    private IEnumerator Dash()
    {
        _isDashing = true;

        yield return new WaitForSeconds(_dashDuration);

        _isDashing = false;
    }

    private void AnimatorControllers()
    {
        _isMoving = _rigidBody.velocity.x != 0;
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("yVelocity", _rigidBody.velocity.y);
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput =    Input.GetAxis("Vertical");

        float playerSpeed = _isDashing ? _dashSpeed : _moveSpeed;

        _rigidBody.velocity = new Vector2(horizontalInput * playerSpeed, _rigidBody.velocity.y);


        if (horizontalInput > 0 && !_facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
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

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            new Vector3(transform.position.x, transform.position.y - _groundCheckDistance, 0)
        );
    }
}
