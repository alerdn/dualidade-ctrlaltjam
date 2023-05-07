using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectionHandler : MonoBehaviour
{
    public event Action OnSpottedPlayer;

    [Header("UI")]
    [SerializeField] private Image _alertCase;
    [SerializeField] private Image _alertMeter;

    [Header("Setup")]
    [SerializeField] private float _secondsToSpotPlayer = 2f;
    [SerializeField] private List<EnemyDetection> _detections;

    private Tween _spottingPlayer;

    private void Start()
    {
        _detections.ForEach(detection =>
        {
            detection.OnNoticeSomething += SpotPlayer;
            detection.OnLostPlayer += LostPlayer;
        });
    }

    private void SpotPlayer(EnemyDetection detection)
    {
        if (_spottingPlayer != null) return;

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
