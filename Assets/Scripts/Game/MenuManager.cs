using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _title;
    [SerializeField] private Transform _menu;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _creditButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Image _foreground;
    [SerializeField] private Animator _canvaAnimator;

    [Header("Dialog Setup")]
    [SerializeField] private GameObject _dialog;
    [SerializeField] private Button _quitDialogButton;

    [Header("Entities")]
    [SerializeField] private GameObject _char;

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);

        _dialog.SetActive(false);
        _quitDialogButton.onClick.AddListener(() => StartCoroutine(CloseDialog()));
    }

    private void StartGame()
    {
        _playButton.interactable = false;
        _creditButton.interactable = false;
        _quitButton.interactable = false;

        _canvaAnimator.SetTrigger("SlideOut");
        _dialog.SetActive(true);
    }

    private IEnumerator CloseDialog()
    {
        _dialog.SetActive(false);
        yield return _char.transform.DOMoveX(20f, 2f).SetRelative();
        _canvaAnimator.SetTrigger("FadeOut");
    }
}
