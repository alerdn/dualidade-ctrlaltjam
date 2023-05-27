using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScreen : MonoBehaviour
{
    [SerializeField] private AbilityDetail _abilityDetail;

    [Header("Stealth")]
    [SerializeField] private SOInt _stealthPoints;
    [SerializeField] private float _line1StealthMaxPoints = 100f;
    [SerializeField] private Image _line1StealthCharge;
    [SerializeField] private float _line2StealthMaxPoints = 100f;
    [SerializeField] private Image _line2StealthCharge;

    [Header("Assassin")]
    [SerializeField] private SOInt _assassinPoints;
    [SerializeField] private float _line1AssassinMaxPoints = 100f;
    [SerializeField] private Image _line1AssassinCharge;
    [SerializeField] private float _line2AssassinMaxPoints = 100f;
    [SerializeField] private Image _line2AssassinCharge;

    private List<AbilityButton> _abilities = new();

    private void OnEnable()
    {
        SelectAbility(null);
    }

    private void Start()
    {
        _abilityDetail.gameObject.SetActive(false);
        _abilities = GetComponentsInChildren<AbilityButton>().ToList();

        _abilities.ForEach(ability => ability.OnClick += SelectAbility);
    }

    private void Update()
    {
        HandleStealthProgress();
        HandleAssassinProgress();
    }

    private void HandleStealthProgress()
    {
        _line1StealthCharge.fillAmount = (float)_stealthPoints.Value * 100f / _line1StealthMaxPoints / 100f;
        _line2StealthCharge.fillAmount = (float)(_stealthPoints.Value - _line1StealthMaxPoints) * 100f / _line2StealthMaxPoints / 100f;


        _abilities.ForEach((ability) =>
        {
            if (ability.AbilityData == null) return;

            if (ability.AbilityData.Type == AbilityType.STEALTH)
            {
                if (ability.AbilityData.State == Ability.AbilityState.UNLOCKED) ability.SetUnlockColor();
            }
        });
    }

    private void HandleAssassinProgress()
    {
        _line1AssassinCharge.fillAmount = (float)_assassinPoints.Value * 100f / _line1AssassinMaxPoints / 100f;
        _line2AssassinCharge.fillAmount = (float)(_assassinPoints.Value - _line1AssassinMaxPoints) * 100f / _line2AssassinMaxPoints / 100f;


        _abilities.ForEach((ability) =>
        {
            if (ability.AbilityData == null) return;

            if (ability.AbilityData.Type == AbilityType.ASSASSIN)
            {
                if (ability.AbilityData.State == Ability.AbilityState.UNLOCKED) ability.SetUnlockColor();
                else ability.SetLockColor();
            }
        });
    }

    private void SelectAbility(Ability data)
    {
        if (data == null)
        {
            _abilityDetail.gameObject.SetActive(false);
            return;
        }

        _abilityDetail.Show(data);
    }
}
