using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightFlick : MonoBehaviour
{
    public enum LightFlickType
    {
        FAIL,
        SMOOTH
    }

    [SerializeField][Range(0f, 1f)] private float _lowerPercentage = .8f;
    [SerializeField] private float _minInterval = 3f;
    [SerializeField] private float _maxInterval = 5f;
    [SerializeField] private LightFlickType _flickType = LightFlickType.FAIL;

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

            yield return _flickType switch
            {
                LightFlickType.FAIL => DOTween
                    .To(() => _light.intensity, (intensity) => _light.intensity = intensity, _light.intensity * _lowerPercentage, .05f)
                    .SetLoops(Random.Range(1, 4) * 2, LoopType.Yoyo)
                    .WaitForCompletion(),

                LightFlickType.SMOOTH => DOTween
                    .To(() => _light.intensity, (intensity) => _light.intensity = intensity, _light.intensity * _lowerPercentage, 2f)
                    .SetLoops(2, LoopType.Yoyo)
                    .WaitForCompletion(),

                _ => null
            };
        }
    }
}
