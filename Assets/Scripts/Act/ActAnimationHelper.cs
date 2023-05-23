using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActAnimationHelper : MonoBehaviour
{
    [SerializeField] private Act _act;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _divider;
    [SerializeField] private Image _background;

    private RectTransform _dividerTransform;

    private void Start()
    {
        _title.text = _act.Title;
        _description.text = _act.Description;

        _title.color = new Color(0f, 0f, 0f, 0f);
        _description.color = new Color(0f, 0f, 0f, 0f);

        _dividerTransform = _divider.GetComponent<RectTransform>();
        _dividerTransform.sizeDelta = new Vector2(0f, 12f);

        StartCoroutine(StartRoutine());
    }

    private IEnumerator StartRoutine()
    {
        DOTween.To(() => _title.fontSize, (fontSize) => _title.fontSize = fontSize, 100, 2f).From();
        yield return DOTween.To(() => _title.color.a, (alpha) => _title.color = new Color(1f, 1f, 1f, alpha), 1f, 1f).WaitForCompletion();

        yield return DOTween
            .To(() => _dividerTransform.sizeDelta.x, (x) => _dividerTransform.sizeDelta = new Vector2(x, 12f), 1270, 1.5f)
            .SetEase(Ease.InCubic)
            .WaitForCompletion();

        DOTween.To(() => _description.fontSize, (fontSize) => _description.fontSize = fontSize, 82, 3f).From();
        yield return DOTween.To(() => _description.color.a, (alpha) => _description.color = new Color(1f, 1f, 1f, alpha), 1f, 2f).WaitForCompletion();

        yield return new WaitForSeconds(1f);

        yield return DOTween.To(() => _background.color.a, (alpha) => _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, alpha), 0f, 5f);
        yield return new WaitForSeconds(1f);

        yield return DOTween.To(() => _description.color.a, (alpha) => _description.color = new Color(1f, 1f, 1f, alpha), 0f, .25f);
        yield return DOTween.To(() => _divider.color.a, (alpha) => _divider.color = new Color(1f, 1f, 1f, alpha), 0f, .45f);
        yield return DOTween.To(() => _dividerTransform.sizeDelta.x, (x) => _dividerTransform.sizeDelta = new Vector2(x, 12f), 0, .45f)
            .SetEase(Ease.InCubic)
            .WaitForCompletion();
        yield return DOTween.To(() => _title.color.a, (alpha) => _title.color = new Color(1f, 1f, 1f, alpha), 0f, .25f).WaitForCompletion();

        gameObject.SetActive(false);
    }
}
