using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsGameOver => _isGameOver;
    public bool IsPlayerLocked { get; private set; }

    [Header("Collectables")]
    [SerializeField] private SOInt _thrownables;
    [SerializeField] private SOInt _knives;
    [SerializeField] private SOInt _keys;

    [Header("Entities")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _enemiesContainer;
    [SerializeField] private DoorHandler _doorHandler;
    [SerializeField] private DuctExit _ductExit;

    [Header("UI")]
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Animator _canvaAnimator;

    private List<Enemy> _enemies;

    private bool _isGameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _thrownables.Value = 0;
        _knives.Value = 3;
        _keys.Value = 0;

        _player.OnKarmaLevelIncreased += OnKarmaLevelIncreased;
        _enemies = _enemiesContainer.GetComponentsInChildren<Enemy>().ToList();
        _enemies.ForEach((enemy) => enemy.DetectionHandler.OnSpottedPlayer += GameOver);

        if (_doorHandler) _doorHandler.OnAreaLeave += OnAreaLeave;
        if (_ductExit) _ductExit.OnAreaLeave += OnAreaLeave;
    }

    private void OnKarmaLevelIncreased(int nextKarmaLevel)
    {
        _enemies.ForEach((enemy) => enemy.DetectionHandler.IncreaseDetectionSpeed(nextKarmaLevel));
    }

    private void OnAreaLeave()
    {
        _canvaAnimator.SetTrigger("LeaveArea");
        IsPlayerLocked = true;
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
