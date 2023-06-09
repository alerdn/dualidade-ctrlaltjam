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
        /// Os pontos s�o zerados aqui por motivo de DEBUG, mas eles j� est�o sendo zerados no menu
        _stealthPoints.Value = 0;
        _assassinPoints.Value = 0;

        _abilities.ForEach((ability) =>
        {
            ability.State = Ability.AbilityState.LOCKED;
        });
    }

#if UNITY_EDITOR
    private void Update()
    {
        CheckAbilityUnlock();
    }
#endif

    public Ability GetAbility(AbilityID id) => _abilities.Find((ability) => ability.ID == id);

    public bool IsAbilityUnlocked(AbilityID id) => GetAbility(id).State == Ability.AbilityState.UNLOCKED;

    public bool CheckAbilityUnlock()
    {
        bool newAbility = false;

        _abilities.ForEach((ability) =>
        {
            if (ability.State == Ability.AbilityState.LOCKED)
            {
                if (ability.Type == AbilityType.STEALTH)
                {
                    if (ability.Cost <= _stealthPoints.Value)
                    {
                        ability.State = Ability.AbilityState.UNLOCKED;
                        newAbility = true;
                    }
                }
                else if (ability.Type == AbilityType.ASSASSIN)
                {
                    if (ability.Cost <= _assassinPoints.Value)
                    {
                        ability.State = Ability.AbilityState.UNLOCKED;
                        newAbility = true;
                    }
                }
            }
        });

        return newAbility;
    }

}
