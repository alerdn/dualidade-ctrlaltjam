using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public event Action<AbilityButton, Ability> OnClick;

    public Ability AbilityData => _abilityData;

    [SerializeField] private Ability _abilityData;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectionImage;

    [Header("Backgorund")]
    [SerializeField] private Color _lockColor;
    [SerializeField] private Color _unLockColor;

    private Image _background;
    private Button _button;

    private void Start()
    {
        _selectionImage.enabled = false;
        _background = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(CallClick);

        if (_abilityData != null)
        {
            _icon.sprite = _abilityData.Icon;
        }
    }

    public void Select()
    {
        _selectionImage.enabled = true;
    }

    public void Deselect()
    {
        _selectionImage.enabled = false;
    }

    public void SetUnlockColor()
    {
        _background.color = _unLockColor;
    }

    public void SetLockColor()
    {
        _background.color = _lockColor;
    }

    private void CallClick()
    {
        if (_abilityData == null) return;
        OnClick?.Invoke(this, _abilityData);
    }
}
