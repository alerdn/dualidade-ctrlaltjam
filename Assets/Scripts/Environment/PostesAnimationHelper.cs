using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostesAnimationHelper : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalMoveX(-90f, 2f).SetLoops(-1).SetEase(Ease.Linear);
    }
}
