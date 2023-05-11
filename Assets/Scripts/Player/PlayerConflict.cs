using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConflict : MonoBehaviour
{
    public event Action OnDefeatEnemy;
    public bool IsEnemyWithinReach => _enemy != null;
    public bool CanExecute = true;

    [SerializeField] private GameObject _interactionIcon;
    [SerializeField] private AudioSource _stabSfx;

    private Enemy _enemy;

    private void Start()
    {
        _interactionIcon.SetActive(false);
    }

    private void Update()
    {
        if (!CanExecute)
        {
            _interactionIcon.SetActive(false);
            _enemy = null;
            return;
        }

        /// Ajuste técnico para não fazer o icone girar
        if (_interactionIcon.activeInHierarchy) _interactionIcon.transform.rotation = Quaternion.identity;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsEnemyWithinReach)
            {
                _enemy.Kill();
                _stabSfx.Play();
                OnDefeatEnemy?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!CanExecute) return;

            _interactionIcon.SetActive(true);
            _enemy = collision.GetComponent<Enemy>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponentInChildren<Enemy>();
        if (enemy)
        {
            _interactionIcon.SetActive(false);
            _enemy = null;
        }
    }
}
