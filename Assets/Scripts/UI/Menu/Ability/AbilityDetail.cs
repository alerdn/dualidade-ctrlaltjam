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

    internal void Show(Ability data)
    {
        _title.text = data.Title;
        _icon.sprite = data.Icon;
        _description.text = data.Description;

        gameObject.SetActive(true);
    }
}
