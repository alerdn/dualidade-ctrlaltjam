using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueV2 _dialogue;
    [SerializeField] private List<EnemyMovement> _enemies;
    [SerializeField] private CinemachineVirtualCamera _vcam;

    private bool _hasTalked;

    void Start()
    {
        _dialogue.OnDialogueFinished += StartEnemyRoutine;
    }

    private void StartEnemyRoutine()
    {
        _vcam.enabled = false;

        _dialogue.gameObject.SetActive(false);
        _enemies.ForEach((enemy) => enemy.StartWalkRoutine());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            if (!_hasTalked)
            {
                StartDialogue();
                _hasTalked = true;
            }
        }
    }

    private void StartDialogue()
    {
        _vcam.enabled = true;
        _dialogue.gameObject.SetActive(true);
    }
}
