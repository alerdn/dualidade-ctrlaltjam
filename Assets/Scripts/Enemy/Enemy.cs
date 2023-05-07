using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyDetectionHandler DetectionHandler => _detectionHandler;

    private EnemyDetectionHandler _detectionHandler;

    private void Awake()
    {
        _detectionHandler = GetComponentInChildren<EnemyDetectionHandler>();
    }
}
