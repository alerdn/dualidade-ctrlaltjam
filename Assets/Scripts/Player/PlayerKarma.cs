using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKarma : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerBodyRenderer;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _karmaSprite;

    private void Start()
    {
        ChangeSprite(_normalSprite);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeSprite();
        }
    }

    public void ChangeSprite(Sprite sprite = null)
    {
        _playerBodyRenderer.sprite = sprite != null ? sprite : (_playerBodyRenderer.sprite == _normalSprite ? _karmaSprite : _normalSprite);
    }
}
