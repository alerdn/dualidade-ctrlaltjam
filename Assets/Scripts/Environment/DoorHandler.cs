using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public event Action OnAreaLeave;

    [SerializeField] private SOInt _keys;
    [SerializeField] private GameObject _interactionIcon;
    [SerializeField] protected Dialogue _dialogue;

    private AudioSource _unlockSound;
    private bool _canOpen;
    private bool _inRange;

    protected virtual void Start()
    {
        _unlockSound = GetComponent<AudioSource>();
        _interactionIcon.SetActive(false);
        _dialogue.OnDialogueFinished += () => _dialogue.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canOpen)
            {
                _keys.Value--;
                _unlockSound.Play();
                LeaveArea();
            }
            else if (_inRange)
            {
                _dialogue.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _interactionIcon.SetActive(true);
            _canOpen = _keys.Value > 0;
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _interactionIcon.SetActive(false);
            _canOpen = false;
            _inRange = false;
        }
    }

    private void LeaveArea()
    {
        OnAreaLeave?.Invoke();
    }
}
