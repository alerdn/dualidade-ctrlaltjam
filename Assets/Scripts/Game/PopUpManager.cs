using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : Static<PopUpManager>
{
    [SerializeField] private TMP_Text _assassinPop;
    [SerializeField] private TMP_Text _stealthPop;

    private void Start()
    {
        _assassinPop.color = new Color(0f, 0f, 0f, 0f);
        _stealthPop.color = new Color(0f, 0f, 0f, 0f);
    }

    public void ShowPoints(int assassinPoints, int stealthPoints)
    {
        _assassinPop.text = assassinPoints > 0 ? $"pontos de assassinato: +{assassinPoints}" : "";
        _stealthPop.text = stealthPoints > 0 ? $"pontos de furtividade: +{stealthPoints}" : "";

        DOTween.To(() => _assassinPop.color.a, (alpha) => _assassinPop.color = new Color(1f, 1f, 1f, alpha), 1f, 2f)
            .SetLoops(2, LoopType.Yoyo);
        DOTween.To(() => _stealthPop.color.a, (alpha) => _stealthPop.color = new Color(1f, 1f, 1f, alpha), 1f, 2f)
            .SetLoops(2, LoopType.Yoyo);
    }

}
