using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyDetectionHandler DetectionHandler => _detectionHandler;

    [SerializeField] private ParticleSystem _deathEffect;

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
        ParticleSystem ps = Instantiate(_deathEffect);
        ps.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        Destroy(gameObject);
    }

    private void OnNoticedSomething(Transform targetLocation)
    {
        _movement.CheckDestination(targetLocation.position);
    }
}
