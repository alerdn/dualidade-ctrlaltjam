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

    public int KarmaLevel => _currentKarmaLevel.Value;

    [Header("Setup")]
    [SerializeField] private int _minEnemiesToChange = 3;
    [SerializeField] private SOInt _currentKarmaLevel;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _playerBodyRenderer;
    [NonReorderable] [SerializeField] private List<Sprite> _sprites;

    private int _enemyDefeated;

    private void Start()
    {
        CheckKarmaLevel();
    }

    public void CheckKarmaLevel()
    {
        int nextLevel = EnemyDefeated / _minEnemiesToChange;
        nextLevel = nextLevel < _sprites.Count ? nextLevel : _sprites.Count - 1;

        if (nextLevel > _currentKarmaLevel.Value) OnKarmaLevelIncreased?.Invoke(nextLevel);

        _currentKarmaLevel.Value = nextLevel;
        _playerBodyRenderer.sprite = _sprites[_currentKarmaLevel.Value];
    }
}
