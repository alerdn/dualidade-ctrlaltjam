using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightFlick : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float _lowerPercentage = .8f;
    [SerializeField] private float _minInterval = 3f;
    [SerializeField] private float _maxInterval = 5f;

    private Light2D _light;

    private void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(LightRoutine());
    }

    private IEnumerator LightRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval));

            yield return DOTween
                .To(() => _light.intensity, (intensity) => _light.intensity = intensity, _light.intensity * _lowerPercentage, .05f)
                .SetLoops(Random.Range(1, 4) * 2, LoopType.Yoyo)
                .WaitForCompletion();
        }
    }
}
