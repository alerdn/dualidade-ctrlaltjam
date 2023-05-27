using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    

    public enum AbilityState
    {
        LOCKED,
        UNLOCKED,
    }

    public AbilityID ID;
    public AbilityState State = AbilityState.LOCKED;
    public string Title;
    public Sprite Icon;
    public string Description;
    public int Cost;
    public AbilityType Type;
}

public enum AbilityID
{
    ARMA_IMPROVISADA,
    ARREMESSO_FATAL,
    GOLPE_AGUCADO,
    PASSOS_DE_PLUMA,
    VEU_DA_NOITE,
    ARREMESSO_CERTEIRO
}

public enum AbilityType
{
    ASSASSIN,
    STEALTH
}