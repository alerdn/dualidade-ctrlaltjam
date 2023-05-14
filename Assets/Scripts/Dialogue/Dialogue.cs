using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Dialogue : MonoBehaviour
{
    public event Action OnDialogueFinished;

    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private TMP_Text _speakerField;
    [SerializeField] private string _speakerName;
    [SerializeField] private List<string> _texts;
    [SerializeField] private float timeBtwChars = 0.1f;

    private AudioSource _typeSound;
    private int _currentTextIndex;
    private bool _isTyping;
    private Coroutine _typingRoutine;

    private void Start()
    {
        if (_speakerName != "") _speakerField.text = _speakerName;

        _typeSound = GetComponent<AudioSource>();
        transform.DOScale(Vector3.zero, .5f).From();

        _nextButton.onClick.AddListener(NextConversation);
        _currentTextIndex = -1;

        NextConversation();
    }

    private void NextConversation()
    {
        if (_isTyping)
        {
            StopCoroutine(_typingRoutine);
            _textField.text = _texts[_currentTextIndex];
            _typeSound.Stop();
            _typingRoutine = null;
            _isTyping = false;
            return;
        }
        _isTyping = true;

        if (_currentTextIndex + 1 > _texts.Count - 1)
        {
            _typingRoutine = null;
            _isTyping = false;
            OnDialogueFinished?.Invoke();
            return;
        }

        _currentTextIndex++;
        _typingRoutine = StartCoroutine(TypeWriterTMP());
    }

    private IEnumerator TypeWriterTMP()
    {
        _textField.text = "";
        _typeSound.Play();
        foreach (char c in _texts[_currentTextIndex])
        {
            _textField.text += c;
            yield return new WaitForSeconds(timeBtwChars);
        }

        _typingRoutine = null;
        _isTyping = false;
    }
}