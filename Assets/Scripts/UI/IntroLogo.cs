using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroLogo : MonoBehaviour
{
    [SerializeField] private List<Image> _logos;
    [SerializeField] private float _timeBetweenLogos;

    private void Start()
    {
        StartCoroutine(ShowLogos());
    }

    private IEnumerator ShowLogos()
    {
        yield return new WaitForSeconds(_timeBetweenLogos);

        foreach (Image logo in _logos)
        {
            Tween tweenLogo = DOTween.To(() => logo.color, (color) => logo.color = color, Color.white, 2f);
            yield return tweenLogo.WaitForCompletion();

            yield return new WaitForSeconds(_timeBetweenLogos);

            tweenLogo = DOTween.To(() => logo.color, (color) => logo.color = color, new Color(1f, 1f, 1f, 0f), 2f);
            yield return tweenLogo.WaitForCompletion();

            yield return new WaitForSeconds(_timeBetweenLogos);
        }

        SceneManager.LoadScene("SCN_Menu");
    }
}
