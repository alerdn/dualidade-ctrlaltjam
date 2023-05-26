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
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _unlockButton;
    [SerializeField] private Color _assassinColor;
    [SerializeField] private Color _stealthColor;

    internal void Show(Ability data)
    {
        _title.text = data.Title;
        _icon.sprite = data.Icon;
        _description.text = data.Description;
        _cost.text = $"{data.Cost} pontos";

        Color selectedColor = data.Type == Ability.AbilityType.ASSASSIN ? _assassinColor : _stealthColor;
        _cost.color = selectedColor;
        _unlockButton.GetComponent<Image>().color = selectedColor;

        gameObject.SetActive(true);
    }
}
