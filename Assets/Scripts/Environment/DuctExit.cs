using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuctExit : MonoBehaviour
{
    public event Action OnAreaLeave;

    [SerializeField] private GameObject _interactionIcon;

    private bool _canEnter;

    private void Start()
    {
        _interactionIcon.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canEnter)
            {
                OnAreaLeave?.Invoke();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _interactionIcon.SetActive(true);
            _canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _interactionIcon.SetActive(false);
            _canEnter = false;
        }
    }
}
