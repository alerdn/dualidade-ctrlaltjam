using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool CanInteract { get; private set; }

    [SerializeField] private GameObject _interactionIcon;

    private void Start()
    {
        _interactionIcon.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (!Player.Instance.IsInteracting)
            {
                CanInteract = true;
                Player.Instance.IsInteracting = true;
                _interactionIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            CanInteract = true;
            Player.Instance.IsInteracting = false;
            _interactionIcon.SetActive(false);
        }
    }
}
