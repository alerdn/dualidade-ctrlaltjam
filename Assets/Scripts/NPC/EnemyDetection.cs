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
    public event Action<EnemyDetection, Player> OnNoticeSomething;
    public event Action<Transform> OnNoticeObject;
    public event Action OnLostPlayer;

    public bool HasNoticedSomething
    {
        get => _hasNoticedSomething;
        private set => _hasNoticedSomething = value;
    }
    public bool HasSpottedPlayer;
    public DetectionType DetectionType => _detectionType;

    [SerializeField] private DetectionType _detectionType;

    [Header("Sight Setup")]
    [SerializeField] private Transform _head;
    [SerializeField] private BoxCollider2D _sightBoxCollider;

    private bool _hasNoticedSomething;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();


        if (player)
        {
            HasNoticedSomething = HandleNoticeSomething(player);
            if (HasNoticedSomething)
            {
                OnNoticeSomething?.Invoke(this, player);
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        bool objectSeen = collision.CompareTag("Thrownable");

        if (player)
        {
            HasNoticedSomething = HandleNoticeSomething(player);
            if (HasNoticedSomething)
            {
                OnNoticeSomething?.Invoke(this, player);
            }
            else
            {
                OnLostPlayer?.Invoke();
            }
        }
        else if (objectSeen)
        {
            OnNoticeObject?.Invoke(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            HasNoticedSomething = false;
            OnLostPlayer?.Invoke();
        }
    }

    private bool HandleNoticeSomething(Player player)
    {
        return _detectionType switch
        {
            DetectionType.SOUND => player.IsProducingSound,
            DetectionType.SIGHT => HandleSightDetection(player),
            _ => true,
        };
    }

    private bool HandleSightDetection(Player player)
    {
        RaycastHit2D hit = Physics2D.Raycast(_head.position, (_head.position - player.Head.position) * -1, 100f, LayerMask.GetMask("Collidable"));
        if (hit)
        {
            return hit.transform.GetComponent<Player>() != null;
        }
        else return false;
    }
}
