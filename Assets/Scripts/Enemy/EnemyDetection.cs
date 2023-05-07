using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectionType
{
    SOUND,
    SIGHT
}

public class EnemyDetection : MonoBehaviour
{
    public event Action<EnemyDetection> OnNoticeSomething;
    public event Action OnLostPlayer;

    public bool HasNoticedSomething
    {
        get => _hasNoticedSomething;
        private set => _hasNoticedSomething = value;
    }
    public bool HasSpottedPlayer;

    [SerializeField] private DetectionType _detectionType;

    [Header("Debug")]
    [SerializeField] public bool _hasNoticedSomething;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            HasNoticedSomething = HandleNoticeSomething(player);
            if (HasNoticedSomething)
            {
                OnNoticeSomething?.Invoke(this);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            HasNoticedSomething = HandleNoticeSomething(player);
            if (HasNoticedSomething)
            {
                OnNoticeSomething?.Invoke(this);
            }
            else
            {
                OnLostPlayer?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            HasNoticedSomething = false;
            OnLostPlayer?.Invoke();
        }
    }

    private bool HandleNoticeSomething(PlayerMovement player)
    {
        switch (_detectionType)
        {
            case DetectionType.SOUND:
                return player.IsProducingSound;
            case DetectionType.SIGHT:
                return true;
            default:
                return true;
        }
    }
}
