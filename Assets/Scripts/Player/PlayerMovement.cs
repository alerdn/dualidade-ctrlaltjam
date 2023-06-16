using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsProducingSound => _isProducingSound;
    public bool IsCrouching => _isCrouching;
    public bool IsRunning => _isRunning;
    public bool CanMove;

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    [Header("Debug")]
    [SerializeField] private float _currentSpeed;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _movementDirection;

    private float _directionalSpeed;
    private bool _isRunning;
    private bool _isCrouching;
    private bool _isProducingSound;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _isProducingSound = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleSprite();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementDirection;
    }

    public void ResetMovement()
    {
        if (IsCrouching) ToggleCrouch();
    }

    private void HandleMovement()
    {
        HandleRun();
        HandleCrouch();

        _currentSpeed = GetSpeed();
        if (!CanMove)
        {
            _currentSpeed = 0;
        }

        _directionalSpeed = Input.GetAxis("Horizontal") * _currentSpeed;
        _movementDirection = _directionalSpeed * Vector2.right;

        HandleProducingSound();
    }

    #region Graphics Helpers

    private void HandleSprite()
    {
        if (_directionalSpeed < 0) transform.rotation = Quaternion.Euler(Vector2.up * 180f);
        else if (_directionalSpeed > 0) transform.rotation = Quaternion.Euler(Vector2.up * 0f);
    }

    private void HandleAnimation()
    {
        if (_directionalSpeed == 0)
        {
            _animator.SetTrigger("Idle");
        }
        else
        {
            _animator.SetTrigger("Walk");
        }
    }

    #endregion

    #region Movement Helpers

    private float GetSpeed()
    {
        float speed = _walkSpeed;
        float speedModifier = 1f;

        if (_isCrouching)
        {
            speedModifier = Player.Instance.AbilityComponent.IsAbilityUnlocked(AbilityID.PASSOS_DE_PLUMA) ? .9f : .5f;
        }

        if (_isRunning)
        {
            speed = _runSpeed;
        }

        return speed * speedModifier;
    }

    private void HandleRun()
    {
        if (!CanMove) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }

    private void HandleCrouch()
    {
        if (!CanMove) return;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
    }

    private void ToggleCrouch()
    {
        _isCrouching = !_isCrouching;
        _animator.SetTrigger("Crouch");
    }

    private void HandleProducingSound()
    {
        if (_directionalSpeed == 0)
        {
            _isProducingSound = false;
        }
        else
        {
            _isProducingSound = !_isCrouching;
        }
    }

    #endregion
}
