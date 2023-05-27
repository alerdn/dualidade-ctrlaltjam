using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public event Action OnKilled;

    public bool IsProtected
    {
        get
        {
            if (Player.Instance.AbilityComponent.IsAbilityUnlocked(AbilityID.GOLPE_PRECISO) && _playerWeaponType == WeaponType.MELEE)
            {
                return false;
            }
            return _isProtected;
        }
    }

    public EnemyDetectionHandler DetectionHandler => _detectionHandler;

    [Header("Interaction")]
    [SerializeField] private GameObject _interactionObject;
    [SerializeField] private Image _interactionIcon;
    [SerializeField] private Sprite _canKillSprite;
    [SerializeField] private Sprite _cannotKillSprite;

    [Space]
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private bool _isProtected;

    private EnemyMovement _movement;
    private EnemyDetectionHandler _detectionHandler;
    private WeaponType _playerWeaponType;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _detectionHandler = GetComponentInChildren<EnemyDetectionHandler>();
        _detectionHandler.OnNoticedSomething += OnNoticedSomething;
    }

    private void Start()
    {
        HideKillIcon();
    }

    private void Update()
    {
        /// Ajuste técnico para não fazer o icone girar
        if (_interactionObject.activeInHierarchy) _interactionObject.transform.rotation = Quaternion.identity;
    }

    public void ShowKillIcon()
    {
        _interactionIcon.sprite = IsProtected ? _cannotKillSprite : _canKillSprite;
        _interactionObject.SetActive(true);
    }

    public void HideKillIcon()
    {
        _interactionObject.SetActive(false);
    }

    public void Kill(WeaponType weaponType)
    {
        _playerWeaponType = weaponType;
        if (IsProtected) return;

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

public enum WeaponType
{
    MELEE,
    RANGED
}