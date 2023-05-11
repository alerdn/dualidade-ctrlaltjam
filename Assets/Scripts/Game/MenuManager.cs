using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _title;
    [SerializeField] private Transform _menu;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Image _foreground;
    [SerializeField] private Animator _canvaAnimator;

    [Header("Dialog Setup")]
    [SerializeField] private Dialogue _dialog;

    [Header("Entities")]
    [SerializeField] private GameObject _char;

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(() => Application.Quit());

        _dialog.gameObject.SetActive(false);
        _dialog.OnDialogueFinished += () => StartCoroutine(CloseDialog());

        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }
    }

    private void StartGame()
    {
        _playButton.interactable = false;
        _quitButton.interactable = false;

        _canvaAnimator.SetTrigger("SlideOut");
        _dialog.gameObject.SetActive(true);
    }

    private IEnumerator CloseDialog()
    {
        _dialog.gameObject.SetActive(false);
        yield return _char.transform.DOMoveX(20f, 2f).SetRelative();
        _canvaAnimator.SetTrigger("FadeOut");
    }
}
