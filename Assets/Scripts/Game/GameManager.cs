using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Static<GameManager>
{
    public bool IsGameOver => _isGameOver;
    public bool IsPlayerLocked { get; private set; }

    [Header("Collectables")]
    [SerializeField] private SOInt _thrownables;
    [SerializeField] private SOInt _knives;
    [SerializeField] private SOInt _keys;

    [Header("Skill points")]
    [SerializeField] private SOInt _assassinPoints;
    [SerializeField] private SOInt _stealthPoints;

    [Header("Entities")]
    [SerializeField] private GameObject _enemiesContainer;
    [SerializeField] private DoorHandler _doorHandler;
    [SerializeField] private DuctExit _ductExit;

    [Header("UI")]
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Animator _canvaAnimator;

    private List<Enemy> _enemies;
    private int _maxEnemyCount;
    private int _currentEnemyCount;
    private bool _isGameOver;

    private void Start()
    {
        _thrownables.Value = 0;
        _knives.Value = 3;
        _keys.Value = 0;
        _currentEnemyCount = 0;

        _enemies = _enemiesContainer.GetComponentsInChildren<Enemy>().ToList();
        _maxEnemyCount = _enemies.Count;
        _enemies.ForEach((enemy) =>
        {
            enemy.DetectionHandler.OnSpottedPlayer += GameOver;
            enemy.OnKilled += UpdateKillCount;
        });

        if (_doorHandler) _doorHandler.OnAreaLeave += OnAreaLeave;
        if (_ductExit) _ductExit.OnAreaLeave += OnAreaLeave;

        Player.Instance.ResetPlayer();
    }

    private void UpdateKillCount()
    {
        _currentEnemyCount++;
    }

    private void OnAreaLeave()
    {
        IsPlayerLocked = true;

        int newAssassinPoints = _currentEnemyCount * 10;
        int newStealthPoints = (_maxEnemyCount - _currentEnemyCount) * 10;

        _assassinPoints.Value += newAssassinPoints;
        _stealthPoints.Value += newStealthPoints;

        PopUpManager.Instance.ShowPoints(newAssassinPoints, newStealthPoints);
        _canvaAnimator.SetTrigger("LeaveArea");
    }

    private void GameOver()
    {
        if (!_isGameOver)
        {
            _isGameOver = true;
            _endScreen.gameObject.SetActive(true);
        }
    }
}
