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

    [Header("Points")]
    [SerializeField] private SOInt _assassinPointsSO;
    [SerializeField] private SOInt _stealthPointsSO;

    [Header("Dialog Setup")]
    [SerializeField] private DialogueV2 _dialogue;

    [Header("Entities")]
    [SerializeField] private GameObject _char;

    private void Start()
    {
        _assassinPointsSO.Value = 0;
        _stealthPointsSO.Value = 0;

        _playButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(() => Application.Quit());

        _dialogue.gameObject.SetActive(false);
        _dialogue.OnDialogueFinished += () => StartCoroutine(CloseDialog());

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
        _dialogue.gameObject.SetActive(true);
    }

    private IEnumerator CloseDialog()
    {
        _dialogue.gameObject.SetActive(false);
        yield return _char.transform.DOMoveX(20f, 2f).SetRelative();
        _canvaAnimator.SetTrigger("FadeOut");
    }
}
