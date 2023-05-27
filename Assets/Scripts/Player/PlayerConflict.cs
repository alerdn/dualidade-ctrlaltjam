using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConflict : MonoBehaviour
{
    public event Action OnDefeatEnemy;
    public bool IsEnemyWithinReach => _enemy != null;
    public bool CanExecute = true;

    [SerializeField] private SOInt _knivesSO;
    [SerializeField] private SOInt _bottlesSO;
    [SerializeField] private AudioSource _stabSfx;

    private Enemy _enemy;
    private bool _isArmaImprovisadaUnlocked;

    private void Update()
    {
        CheckCanExecute();

        if (!CanExecute)
        {
            if (_enemy != null) _enemy.HideKillIcon();
            _enemy = null;
            return;
        }

        if (_enemy != null && _enemy.IsProtected) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsEnemyWithinReach)
            {
                UseWeapon();
                _enemy.Kill(WeaponType.MELEE);
                _stabSfx.Play();
                OnDefeatEnemy?.Invoke();
            }
        }
    }

    private void CheckCanExecute()
    {
        _isArmaImprovisadaUnlocked = Player.Instance.AbilityComponent.IsAbilityUnlocked(AbilityID.ARMA_IMPROVISADA);

        if (_isArmaImprovisadaUnlocked)
        {
            if (_knivesSO.Value <= 0 && _bottlesSO.Value <= 0) CanExecute = false;
        }
        else
        {
            if (_knivesSO.Value <= 0) CanExecute = false;
        }
    }

    private void UseWeapon()
    {
        if (_knivesSO.Value > 0)
        {
            _knivesSO.Value--;
        }
        else if (_isArmaImprovisadaUnlocked)
        {
            _bottlesSO.Value--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            if (!CanExecute) return;

            _enemy = enemy;
            _enemy.ShowKillIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.HideKillIcon();
            _enemy = null;
        }
    }
}
