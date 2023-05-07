using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    public event Action OnBehindObstacle;

    public bool IsHiddenInside { get; private set; }

    private bool _canHide;
    private Transform _hidingPlace;
    private List<SpriteRenderer> _sprites;

    private void Awake()
    {
        _sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!IsHiddenInside && _canHide)
            {
                Hide();
            }
            else if (IsHiddenInside)
            {
                LeaveHiding();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            OnBehindObstacle?.Invoke();
        }
        else if (collision.CompareTag("HidingPlace"))
        {
            _canHide = true;
            _hidingPlace = collision.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            OnBehindObstacle?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            LeaveHiding();
        }
        else if (collision.CompareTag("HidingPlace"))
        {
            _canHide = false;
            _hidingPlace = null;
        }
    }

    private void Hide()
    {
        IsHiddenInside = true;
        transform.position = new Vector2(_hidingPlace.position.x, transform.position.y);
        gameObject.layer = LayerMask.NameToLayer("Default");
        ToggleSprites(false);
    }

    private void LeaveHiding()
    {
        IsHiddenInside = false;
        gameObject.layer = LayerMask.NameToLayer("Collidable");
        ToggleSprites(true);
    }

    private void ToggleSprites(bool show)
    {
        _sprites.ForEach((sprite) => sprite.enabled = show);
    }
}
