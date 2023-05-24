using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTutorialHelper : DoorHandler
{
    [SerializeField] private DuctExit _duct;

    protected override void Start()
    {
        base.Start();
        _duct.GetComponent<BoxCollider2D>().enabled = false;
        _dialogue.OnDialogueFinished += () => _duct.GetComponent<BoxCollider2D>().enabled = true;
    }
}
