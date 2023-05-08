using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsProducingSound => _movementComponent.IsProducingSound;
    public bool IsRunning => _movementComponent.IsRunning;
    public Transform Head => _head;

    [SerializeField] private Transform _head;

    private PlayerMovement _movementComponent;
    private PlayerStealth _stealthComponent;

    private bool _canHide;

    private void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        _stealthComponent = GetComponent<PlayerStealth>();

        _stealthComponent.OnBehindObstacle += TakeCover;
    }

    private void Update()
    {
        if (_stealthComponent.IsHiddenInside)
        {
            _movementComponent.CanMove = false;
        }
        else
        {
            _movementComponent.CanMove = true;
        }
    }

    private void TakeCover()
    {
        if (_movementComponent.IsCrouching)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Collidable");
        }
    }
}
