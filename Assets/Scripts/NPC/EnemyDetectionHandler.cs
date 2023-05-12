using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectionHandler : MonoBehaviour
{
    public event Action OnSpottedPlayer;
    public event Action<Transform> OnNoticedSomething;

    public EnemyDetection SightDetection => _detections.Find((detection) => detection.DetectionType == DetectionType.SIGHT);

    [Header("UI")]
    [SerializeField] private Image _alertCase;
    [SerializeField] private Image _alertMeter;

    [Header("Setup")]
    [SerializeField] private float _secondsToSpotPlayer = 3f;
    [SerializeField] private float _minSecondsToSpotPlayer = .25f;
    [SerializeField] private float _minDistanceToAutoSpot = 4f;
    [SerializeField] private List<EnemyDetection> _detections;

    private Tween _spottingPlayer;

    private void Start()
    {
        _detections.ForEach(detection =>
        {
            detection.OnNoticeObject += OnNoticedSomething;
            detection.OnNoticeSomething += SpotPlayer;
            detection.OnLostPlayer += LostPlayer;
        });
    }

    public void IncreaseDetectionSpeed(int karmaLevel)
    {
        float newTimeToSpotPlayer = _secondsToSpotPlayer - karmaLevel;
        _secondsToSpotPlayer = newTimeToSpotPlayer > _minSecondsToSpotPlayer ? newTimeToSpotPlayer : _minSecondsToSpotPlayer;
    }

    private void SpotPlayer(EnemyDetection detection, Player player)
    {
        if (_spottingPlayer != null)
        {
            if (player.IsRunning || Vector2.Distance(transform.position, player.transform.position) < _minDistanceToAutoSpot) _spottingPlayer.Complete();
            return;
        }

        OnNoticedSomething?.Invoke(player.transform);

        _alertCase.enabled = true;
        _alertMeter.fillAmount = 0f;

        _spottingPlayer = DOTween.To(() => _alertMeter.fillAmount, (amount) => _alertMeter.fillAmount = amount, 1f, _secondsToSpotPlayer).OnComplete(() =>
        {
            detection.HasSpottedPlayer = detection.HasNoticedSomething;
            if (!detection.HasSpottedPlayer)
            {
                _spottingPlayer = null;
            }
            else
            {
                OnSpottedPlayer?.Invoke();
            }
        });
    }

    private void LostPlayer()
    {
        // Só perde o player de vista de ele saiu da área de todas as detecções
        if (!_detections.TrueForAll((detection) => detection.HasNoticedSomething == false)) return;

        _spottingPlayer?.Kill();
        _spottingPlayer = null;

        _alertCase.enabled = false;
        _alertMeter.fillAmount = 0f;
    }
}
