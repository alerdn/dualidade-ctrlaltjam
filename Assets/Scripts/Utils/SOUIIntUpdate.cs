using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SOInt _soInt;

    void Start()
    {
        _text.text = _soInt.Value.ToString();
    }

    void Update()
    {
        _text.text = _soInt.Value.ToString();
    }
}
