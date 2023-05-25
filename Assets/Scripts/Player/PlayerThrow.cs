using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public bool CanThrow = true;

    [SerializeField] private Thrownable _thrownableItem;
    [SerializeField] private GameObject _interactionIcon;

    [Header("Setup")]
    [SerializeField] private SOInt _thrownablesSO;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed = 10f;

    private Thrownable _thrownable;
    private bool _readyToThrow;

    private void Start()
    {
        _interactionIcon.SetActive(false);
    }

    private void Update()
    {
        if (!CanThrow)
        {
            Cursor.visible = true;
            _interactionIcon.SetActive(false);
            if (_thrownable != null) Destroy(_thrownable);
            return;
        }

        HandleThrownMode();
        HandleThrowIcon();

        if (_thrownable != null) _thrownable.transform.position = new Vector2(transform.position.x - 1, transform.position.y + 1);

        ThrowItem();
    }

    private void HandleThrownMode()
    {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_thrownablesSO.Value <= 0) return;

            _readyToThrow = !_readyToThrow;

            if (_readyToThrow)
            {
                Cursor.visible = false;
                _interactionIcon.SetActive(true);
                _thrownable = Instantiate(_thrownableItem, transform);
            }
            else
            {
                Cursor.visible = true;
                _interactionIcon.SetActive(false);
                Destroy(_thrownable);
            }
        }
    }

    private void HandleThrowIcon()
    {
        if (_interactionIcon.activeInHierarchy)
        {
            _interactionIcon.transform.rotation = Quaternion.identity;
            if (_readyToThrow)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 1f;
                _interactionIcon.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            }
            else
            {
                Cursor.visible = true;
                _interactionIcon.SetActive(false);
            }
        }
    }

    private void ThrowItem()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_readyToThrow)
            {
                _readyToThrow = false;
                _thrownablesSO.Value--;
                _thrownable.Throw(_speed);
                _thrownable = null;
            }
        }
    }
}
