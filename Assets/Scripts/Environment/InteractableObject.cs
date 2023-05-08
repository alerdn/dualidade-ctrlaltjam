using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject _interactionIcon;

    private void Start()
    {
        _interactionIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactionIcon.SetActive(true);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        _interactionIcon.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactionIcon.SetActive(false);
    }
}
