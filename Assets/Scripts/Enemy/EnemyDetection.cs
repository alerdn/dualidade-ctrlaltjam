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
    [SerializeField] private float _boxWidthBase = 4f;
    [SerializeField] private float _boxWidthMax = 14f;

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

    public void IncreaseSightCollider(int karmaLevel)
    {
        if (_sightBoxCollider == null) return;

        float xBase = _boxWidthBase * ((float)karmaLevel + 1f);
        xBase = xBase < _boxWidthMax ? xBase : _boxWidthMax;

        _sightBoxCollider.size = new Vector2(xBase, 4f);
        _sightBoxCollider.offset = new Vector2(xBase / 1.875f, 0f);
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
