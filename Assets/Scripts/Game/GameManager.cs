using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _enemiesContainer;

    private List<Enemy> _enemies;

    private bool _isGameOver;

    private void Start()
    {
        _enemies = _enemiesContainer.GetComponentsInChildren<Enemy>().ToList();

        _player.OnKarmaLevelIncreased += OnKarmaLevelIncreased;
        _enemies.ForEach((enemy) => enemy.DetectionHandler.OnSpottedPlayer += GameOver);
    }

    private void OnKarmaLevelIncreased(int nextKarmaLevel)
    {
        _enemies.ForEach((enemy) => enemy.DetectionHandler.SightDetection.IncreaseSightCollider(nextKarmaLevel));
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
