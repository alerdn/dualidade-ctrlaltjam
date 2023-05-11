using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaTutorialHelper : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    private bool _hasShownMessage;

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _dialogue.OnDialogueFinished += () => _dialogue.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && !_hasShownMessage)
        {
            if (player.EnemyDefeated > 0)
            {
                _dialogue.gameObject.SetActive(true);
                _hasShownMessage = true;
            }
        }
    }
}
