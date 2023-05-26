using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public event Action<int> OnClick;

    [SerializeField] private Image _underline;

    private int _id;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Click);
    }

    public void Init(int id)
    {
        _id = id;
    }

    private void Click()
    {
        OnClick?.Invoke(_id);
    }

    public void Select()
    {
        _underline.enabled = true;
    }

    public void Deselect()
    {
        _underline.enabled = false;
    }
}
