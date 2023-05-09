using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Collectables")]
    [SerializeField] private SOInt _thrownables;
    [SerializeField] private SOInt _keys;

    [Header("Entities")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _enemiesContainer;

    private List<Enemy> _enemies;

    private bool _isGameOver;

    private void Start()
    {
        _thrownables.Value = 0;
        _keys.Value = 0;

        _player.OnKarmaLevelIncreased += OnKarmaLevelIncreased;
        _enemies = _enemiesContainer.GetComponentsInChildren<Enemy>().ToList();
        _enemies.ForEach((enemy) => enemy.DetectionHandler.OnSpottedPlayer += GameOver);
    }

    private void OnKarmaLevelIncreased(int nextKarmaLevel)
    {
        _enemies.ForEach((enemy) => enemy.DetectionHandler.IncreaseDetectionSpeed(nextKarmaLevel));
    }

    private void GameOver()
    {
        if (!_isGameOver)
        {
            _isGameOver = true;
            Debug.Log("VOCÊ FOI DETECTADA!");
        }
    }
}
