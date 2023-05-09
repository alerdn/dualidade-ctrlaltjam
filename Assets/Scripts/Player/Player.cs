using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> OnKarmaLevelIncreased;

    public bool IsProducingSound => _movementComponent.IsProducingSound;
    public bool IsRunning => _movementComponent.IsRunning;
    public Transform Head => _head;
    public int KarmaLevel => _karmaComponent.KarmaLevel;

    [SerializeField] private Transform _head;

    private PlayerMovement _movementComponent;
    private PlayerStealth _stealthComponent;
    private PlayerConflict _conflictComponent;
    private PlayerKarma _karmaComponent;

    private bool _canHide;

    private void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        _stealthComponent = GetComponent<PlayerStealth>();
        _conflictComponent = GetComponent<PlayerConflict>();
        _karmaComponent = GetComponent<PlayerKarma>();

        _stealthComponent.OnBehindObstacle += TakeCover;
        _conflictComponent.OnDefeatEnemy += OnDefeatEnemy;
        _karmaComponent.OnKarmaLevelIncreased += OnKarmaLevelIncreased;
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

    private void OnDefeatEnemy()
    {
        _karmaComponent.EnemyDefeated++;
    }
}
