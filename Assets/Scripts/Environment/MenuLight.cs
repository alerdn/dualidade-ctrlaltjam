using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLight : MonoBehaviour
{
    [SerializeField] private GameObject _lightPrefab;
    [SerializeField] private int _quantity;
    [SerializeField] private int _spaceBetween;

    private void Start()
    {
        for (int i = 0; i < _quantity; i++)
        {
            GameObject poste = Instantiate(_lightPrefab, transform);
            poste.transform.position = new Vector3(i * _spaceBetween, 0f, 0f);
        }
    }
}
