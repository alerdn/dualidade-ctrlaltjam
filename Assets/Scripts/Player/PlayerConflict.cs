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
    [SerializeField] private AudioSource _stabSfx;

    private Enemy _enemy;

    private void Update()
    {
        if (_knivesSO.Value <= 0) CanExecute = false;
        if (_enemy != null && _enemy.IsProtected) CanExecute = false;

        if (!CanExecute)
        {
            if (_enemy != null) _enemy.HideKillIcon();
            _enemy = null;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsEnemyWithinReach)
            {
                _knivesSO.Value--;
                _enemy.Kill();
                _stabSfx.Play();
                OnDefeatEnemy?.Invoke();
            }
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
