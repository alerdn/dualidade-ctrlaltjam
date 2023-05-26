using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityScreen : MonoBehaviour
{
    [SerializeField] private AbilityDetail _abilityDetail;

    private List<AbilityButton> _abilities;

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
