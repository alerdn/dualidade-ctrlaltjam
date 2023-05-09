using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKarma : MonoBehaviour
{
    public event Action<int> OnKarmaLevelIncreased;

    public int EnemyDefeated
    {
        get => _enemyDefeated;
        set
        {
            _enemyDefeated = value;
            CheckKarmaLevel();
        }
    }

    public int KarmaLevel => _currentKarmaLevel;

    [Header("Setup")]
    [SerializeField] private int _minEnemiesToChange = 3;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _playerBodyRenderer;
    [NonReorderable] [SerializeField] private List<Sprite> _sprites;

    private int _enemyDefeated;
    private int _currentKarmaLevel;

    private void Start()
    {
        CheckKarmaLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EnemyDefeated++;
        }
    }

    public void CheckKarmaLevel()
    {
        int nextLevel = EnemyDefeated / _minEnemiesToChange;
        nextLevel = nextLevel < _sprites.Count ? nextLevel : _sprites.Count - 1;

        if (nextLevel > _currentKarmaLevel) OnKarmaLevelIncreased?.Invoke(nextLevel);

        _currentKarmaLevel = nextLevel;
        _playerBodyRenderer.sprite = _sprites[_currentKarmaLevel];
    }
}
