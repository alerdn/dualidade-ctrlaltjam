using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;

    private bool _isGameOver;

    private void Start()
    {
        _enemies.ForEach((enemy) => enemy.DetectionHandler.OnSpottedPlayer += GameOver);
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
