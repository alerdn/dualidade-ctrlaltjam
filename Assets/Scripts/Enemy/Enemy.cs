using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyDetectionHandler DetectionHandler => _detectionHandler;

    private EnemyMovement _movement;
    private EnemyDetectionHandler _detectionHandler;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _detectionHandler = GetComponentInChildren<EnemyDetectionHandler>();
        _detectionHandler.OnNoticedSomething += OnNoticedSomething;
    }

    private void OnNoticedSomething(Transform playerLocation)
    {
       _movement.CheckDestination(playerLocation.position);
    }
}
