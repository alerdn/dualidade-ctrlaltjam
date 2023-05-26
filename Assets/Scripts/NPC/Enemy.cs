using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnKilled;

    public bool IsProtected => _isProtected;
    public EnemyDetectionHandler DetectionHandler => _detectionHandler;

    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private bool _isProtected;

    private EnemyMovement _movement;
    private EnemyDetectionHandler _detectionHandler;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _detectionHandler = GetComponentInChildren<EnemyDetectionHandler>();
        _detectionHandler.OnNoticedSomething += OnNoticedSomething;
    }

    public void Kill()
    {
        if (_isProtected) return;

        ParticleSystem ps = Instantiate(_deathEffect);
        ps.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        OnKilled?.Invoke();
        Destroy(gameObject);
    }

    private void OnNoticedSomething(Transform targetLocation)
    {
        _movement.CheckDestination(targetLocation.position);
    }
}
