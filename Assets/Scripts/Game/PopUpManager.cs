using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : Static<PopUpManager>
{
    [SerializeField] private TMP_Text _assassinPop;
    [SerializeField] private TMP_Text _stealthPop;
    [SerializeField] private TMP_Text _abilityPop;

    private void Start()
    {
        _assassinPop.color = new Color(_assassinPop.color.r, _assassinPop.color.g, _assassinPop.color.b, 0f);
        _stealthPop.color = new Color(_stealthPop.color.r, _stealthPop.color.g, _stealthPop.color.b, 0f);
        _abilityPop.color = new Color(0f, 0f, 0f, 0f);
    }

    public void ShowPoints(int assassinPoints, int stealthPoints)
    {
        if (Player.Instance.AbilityComponent.CheckAbilityUnlock())
        {
            DOTween.To(() => _abilityPop.color.a, (alpha) => _abilityPop.color = new Color(1f, 1f, 1f, alpha), 1f, 4f)
            .SetLoops(2, LoopType.Yoyo).SetUpdate(true);
        }

        _assassinPop.text = assassinPoints > 0 ? $"pontos de assassinato: +{assassinPoints}" : "";
        _stealthPop.text = stealthPoints > 0 ? $"pontos de furtividade: +{stealthPoints}" : "";

        DOTween.To(() => _assassinPop.color.a, (alpha) => _assassinPop.color = new Color(_assassinPop.color.r, _assassinPop.color.g, _assassinPop.color.b, alpha), 1f, 2f)
            .SetLoops(2, LoopType.Yoyo).SetUpdate(true);
        DOTween.To(() => _stealthPop.color.a, (alpha) => _stealthPop.color = new Color(_stealthPop.color.r, _stealthPop.color.g, _stealthPop.color.b, alpha), 1f, 2f)
            .SetLoops(2, LoopType.Yoyo).SetUpdate(true);
    }

}
