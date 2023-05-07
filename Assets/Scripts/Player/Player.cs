using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsProducingSound => _movement.IsProducingSound;
    public Transform Head => _head;

    [SerializeField] private Transform _head;

    private PlayerMovement _movement;

    private void Start()
    {
        _movement = GetComponentInChildren<PlayerMovement>();
    }
}
