using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private SOInt _assassinPoints;
    [SerializeField] private SOInt _stealthPoints;

    [Header("Abilities")]
    [SerializeField] private List<Ability> _abilities;

    private void Start()
    {
        /// Os pontos são zerados aqui por motivo de DEBUG, mas eles já estão sendo zerados no menu
        _stealthPoints.Value = 0;
        _assassinPoints.Value = 0;

        _abilities.ForEach((ability) =>
        {
            ability.State = Ability.AbilityState.LOCKED;
        });
    }

    private void Update()
    {
        CheckAbilityUnlock();
    }
    
    public Ability GetAbility(AbilityID id) => _abilities.Find((ability) => ability.ID == id);

    public bool IsAbilityUnlocked(AbilityID id) => GetAbility(id).State == Ability.AbilityState.UNLOCKED;

    private void CheckAbilityUnlock()
    {
        _abilities.ForEach((ability) =>
        {
            if (ability.Type == AbilityType.STEALTH)
            {
                if (ability.Cost <= _stealthPoints.Value)
                {
                    ability.State = Ability.AbilityState.UNLOCKED;
                }
            }
            else if (ability.Type == AbilityType.ASSASSIN)
            {
                if (ability.Cost <= _assassinPoints.Value)
                {
                    ability.State = Ability.AbilityState.UNLOCKED;
                }
            }
        });
    }

}
