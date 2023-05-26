using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public event Action<Ability> OnClick;

    [SerializeField] private Ability _abilityData;
    [SerializeField] private Image _icon;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(CallClick);

        if (_abilityData != null)
        {
            _icon.sprite = _abilityData.Icon;
        }
    }

    private void CallClick()
    {
        if (_abilityData == null) return;
        OnClick?.Invoke(_abilityData);
    }
}
