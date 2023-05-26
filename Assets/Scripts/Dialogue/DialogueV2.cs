using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueV2 : MonoBehaviour
{
    public event Action OnDialogueFinished;

    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private TMP_Text _speakerField;
    [SerializeField] private Image _speakerImage;
    [SerializeField] private List<DialogueData> _dialogueDatas;
    [SerializeField] private float timeBtwChars = 0.05f;

    private AudioSource _typeSound;
    private int _currentTextIndex;
    private int _currentDialogueIndex;
    private bool _isTyping;
    private Coroutine _typingRoutine;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        _typeSound = GetComponent<AudioSource>();
        transform.DOScale(Vector3.zero, .5f).From().SetUpdate(true);

        _nextButton.onClick.AddListener(NextConversation);
        _currentDialogueIndex = 0;
        _currentTextIndex = -1;

        NextConversation();
    }

    private void NextConversation()
    {
        if (_isTyping)
        {
            StopCoroutine(_typingRoutine);
            _textField.text = _dialogueDatas[_currentDialogueIndex]._texts[_currentTextIndex];
            _typeSound.Stop();
            _typingRoutine = null;
            _isTyping = false;
            return;
        }
        _isTyping = true;

        /// Muda o speaker
        if (_currentTextIndex + 1 > _dialogueDatas[_currentDialogueIndex]._texts.Count - 1)
        {
            if (_currentDialogueIndex + 1 >= _dialogueDatas.Count)
            {
                _typingRoutine = null;
                _isTyping = false;
                OnDialogueFinished?.Invoke();
                return;
            }

            _speakerImage.transform.DOScale(0f, .25f).From().SetUpdate(true);
            _currentDialogueIndex++;
            _currentTextIndex = -1;
        }

        _currentTextIndex++;
        _speakerField.text = _dialogueDatas[_currentDialogueIndex].speakerName;
        _speakerImage.sprite = _dialogueDatas[_currentDialogueIndex].speakerSprite;

        _typingRoutine = StartCoroutine(TypeWriterTMP());
    }

    private IEnumerator TypeWriterTMP()
    {
        _textField.text = "";
        _typeSound.Play();
        foreach (char c in _dialogueDatas[_currentDialogueIndex]._texts[_currentTextIndex])
        {
            _textField.text += c;
            yield return new WaitForSecondsRealtime(timeBtwChars);
        }

        _typeSound.Stop();
        _typingRoutine = null;
        _isTyping = false;
    }
}