using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDetail : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _costState;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _stealthColor;
    [SerializeField] private Color _assassinColor;

    internal void Show(Ability data)
    {
        _title.text = data.Title;
        _icon.sprite = data.Icon;
        _description.text = data.Description;

        if (data.State == Ability.AbilityState.LOCKED)
        {
            _costState.text = $"pontos requeridos: {data.Cost}";
            _costState.color = _defaultColor;
        }
        else
        {
            _costState.text = "habilitada";
            _costState.color = data.Type == AbilityType.STEALTH ? _stealthColor : _assassinColor;
        }

        gameObject.SetActive(true);
    }
}
