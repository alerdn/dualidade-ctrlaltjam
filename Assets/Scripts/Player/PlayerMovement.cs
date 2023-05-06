using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _runSpeed;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _movementDirection;
    private float _currentSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float directionSpeed = HandleMovement();
        HandleSprite(directionSpeed);
        HandleAnimation(directionSpeed);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movementDirection;
    }

    private float HandleMovement()
    {
        _currentSpeed = _movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _runSpeed;
        }

        float directionalSpeed = Input.GetAxis("Horizontal") * _currentSpeed;
        _movementDirection = directionalSpeed * Vector2.right;

        return directionalSpeed;
    }

    private void HandleSprite(float direction)
    {
        if (direction < 0) transform.rotation = Quaternion.Euler(Vector2.up * 180f);
        else if (direction > 0) transform.rotation = Quaternion.Euler(Vector2.up * 0f);
    }

    private void HandleAnimation(float direction)
    {
        if (direction == 0)
        {
            _animator.SetTrigger("Idle");
        }
        else
        {
            _animator.SetTrigger("Walk");
        }
    }
}
