using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public event Action<int> OnKarmaLevelIncreased;

    public bool IsProducingSound => _movementComponent.IsProducingSound;
    public bool IsRunning => _movementComponent.IsRunning;
    public Transform Head => _head;
    public int KarmaLevel => _karmaComponent.KarmaLevel;
    public int EnemyDefeated => _karmaComponent.EnemyDefeated;

    [SerializeField] private Transform _head;

    private PlayerMovement _movementComponent;
    private PlayerStealth _stealthComponent;
    private PlayerConflict _conflictComponent;
    private PlayerKarma _karmaComponent;
    private PlayerThrow _throwComponent;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance.transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        _stealthComponent = GetComponent<PlayerStealth>();
        _conflictComponent = GetComponent<PlayerConflict>();
        _karmaComponent = GetComponent<PlayerKarma>();
        _throwComponent = GetComponent<PlayerThrow>();

        _stealthComponent.OnBehindObstacle += TakeCover;
        _conflictComponent.OnDefeatEnemy += OnDefeatEnemy;
        _karmaComponent.OnKarmaLevelIncreased += OnKarmaLevelIncreased;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlayerLocked || GameManager.Instance.IsGameOver)
        {
            _movementComponent.CanMove = false;
            _conflictComponent.CanExecute = false;
            _throwComponent.CanThrow = false;

            return;
        }

        IsHiddenCheck();
    }

    private void IsHiddenCheck()
    {
        if (_stealthComponent.IsHiddenInside)
        {
            _movementComponent.CanMove = false;
            _conflictComponent.CanExecute = false;
            _throwComponent.CanThrow = false;
        }
        else if (_stealthComponent.IsHiddenBehind)
        {
            _movementComponent.CanMove = true;
            _conflictComponent.CanExecute = false;
            _throwComponent.CanThrow = true;
        }
        else
        {
            _movementComponent.CanMove = true;
            _conflictComponent.CanExecute = true;
            _throwComponent.CanThrow = true;
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
