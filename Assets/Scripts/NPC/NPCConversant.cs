using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConversant : MonoBehaviour
{
    [SerializeField] private DialogueV2 _dialogue;
    [SerializeField] private GameObject _interactionIcon;
    [SerializeField] private bool _autoTalk;

    private bool _canTalk;

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _dialogue.OnDialogueFinished += FinishDialogue;

        _interactionIcon.SetActive(false);
    }

    private void Update()
    {
        /// Ajuste técnico para não fazer o icone girar
        if (_interactionIcon.activeInHierarchy) _interactionIcon.transform.rotation = Quaternion.identity;

        if (_canTalk)
        {
            _interactionIcon.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) || _autoTalk)
            {
                _canTalk = false;
                _autoTalk = false;
                _dialogue.gameObject.SetActive(true);
            }
        }
        else
        {
            _interactionIcon.SetActive(false);
        }
    }

    private void FinishDialogue()
    {
        _dialogue.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _canTalk = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            _canTalk = false;
        }
    }
}
